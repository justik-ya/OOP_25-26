using System;
using System.Collections.Generic;

namespace Task2
{
    /*
     * Это интерфейс управляемого ресурса заводится для синхронизации
     * методов самого ресурса и декоратора средствами компилятора
     */
    public interface IManagedResource : IDisposable
    {
        Int32 ResourceId { get; }
    }

    /*
     * Это наш управляемый ресурс
     */
    public class ManagedResource : IManagedResource
    {
        public ManagedResource( Int32 id )
        {
            ResourceId = id;
            Console.WriteLine( $"Resource {id} created" );
        }

        #region IManagedResource

        public Int32 ResourceId { get; }

        #region IDisposable

        public void Dispose()
        {
            Console.WriteLine( $"Resource {ResourceId} disposed" );
        }

        #endregion

        #endregion
    }

    /*
     * Это декоратор для нашего ресурса который будет отдавать
     * объект в пул, после работы с ним
     */
    public class ManagedResourceHolder : IManagedResource
    {
        private readonly ResourcePool _pool;
        private readonly IManagedResource _resource;

        public ManagedResourceHolder( ResourcePool pool, IManagedResource resource )
        {
            _pool = pool;
            _resource = resource;
            Console.WriteLine( $"Resource {resource.ResourceId} acquired" );
        }

        #region IManagedResource

        // В данном случае декоратор выполняет роль proxy и просто перенаправляет все
        // интерфейсные вызовы самому ресурсу, гарантия существования аналогичного
        // метода предоставляется интерфейсом, который наследует и ресурс и декоратор
        public Int32 ResourceId => _resource.ResourceId;

        #region IDisposable

        public void Dispose()
        {
            // Здесь мы возвращаем ресурс обратно в пул, когда он больше не нужен
            Console.WriteLine( $"Resource {_resource.ResourceId} released" );
            _pool.Release( _resource );
        }

        #endregion

        #endregion
    }


    /*
     * В данном задании потребуется реализовать логику пула объектов, а именно:
     * 1. Хранение объектов, выбирете подходящую коллекцию самостоятельно.
     * 2. Логику создания и выдачи ресурса наружу в методе Rent
     * 3. Логику возврата ресурса в пул объектов в методе Release
     * 4. И логику разрушения самого пула вместе с теми объектами что в нем хранятся.
     *    Разрушение производится путем явного вызова метода Dispose() у каждого
     *    ресурсного объекта.
     * 
     * Так же вот дополнительные условия, чтобы отработать корректно тесты:
     * 1. Если при аренде ресурса, свободных ресурсов в пуле нет, то создаем новый и возвращаем его.
     *    Если свободный ресурс есть, тогда возвращаем первый в списке.
     * 2. Объекты которые были возвращены в пул первыми должны быть отданы при новой аренде также первыми.
     *    Прозрачный намек на очередь :) для хранения объектов.
     * 3. Каждый новый ресурс который будет создаваться должен нумероваться по порядку начиная с 1.
     *    Номер ресурса передается ему в конструктор при его создании.
     * 4. Ресурсы должны разрушаться в порядке, в котором они размещены в очереди, т.е. в том порядке
     *    в котором их выдавал бы метод Rent при их запросе
     * 5. Ресурсы которые были арендованы, но не возвращены в пул так же должны быть разрушены при
     *    разрушении пула
     * 6. Утекшие ресурсы (которые не были возвращены в пул) можно разрушать в произвольном порядке
     *    тест будет выполняться только для одного утекшего объекта, но их может быть много и этот
     *    момент я в реализации проверю :) по коду
     *
     * FYI: После реализации данного класса, можете смело положить его к себе в копилку (архив) ;)
     *      Гарантирую, что пригодится в будущем.
     */
    public class ResourcePool : IDisposable
    {
        private readonly Queue<IManagedResource> _freeResources;
        private readonly List<IManagedResource> _allResources;
        private int _nextId;
        
        public ResourcePool()
        {
            _freeResources = new Queue<IManagedResource>();
            _allResources = new List<IManagedResource>();
            _nextId = 1;
        }

        public IManagedResource Rent()
        {
            IManagedResource resource;

            if (_freeResources.Count > 0)
                resource = _freeResources.Dequeue();
            else
            {
                resource = new ManagedResource(_nextId);
                _nextId++;
                _allResources.Add(resource);
            }

            return new ManagedResourceHolder(this, resource);

        }

        public void Release( IManagedResource resource )
        {
            _freeResources.Enqueue(resource);
        }

        #region IDisposable

        public void Dispose()
        {
            for (int i = _allResources.Count - 1; i >= 0; i--)
                _allResources[i].Dispose();

            
            _freeResources.Clear();
            _allResources.Clear();
        }

        #endregion
    }
}
