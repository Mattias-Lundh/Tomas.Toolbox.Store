using Store_tests;
using Toolbox.Store.Components;

namespace Store_tests
{
    public class NavigatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void FindEntityInCollectionAndNested()
        {
            // arrange

            var tomato = new TomatoEntity();
            var id = tomato.Id;

            var table = new TableEntity();
            table.Bowl = new BowlEntity();
            table.Bowl.Tomato = tomato;

            var store = new SalladStore();        
            store.Tables.Add(table);
            
            // act
            var navigator = new Navigator(store);

            var result = navigator.FindEntity(id);

            //assert
            Assert.That(tomato, Is.EqualTo(result));
        }
        [Test]
        public void FindEntityAsProp()
        {
            // arrange

            var tomato = new TomatoEntity();
            var id = tomato.Id;

            var store = new SalladStore();
            store.Tomato = tomato;

            // act

            var navigator = new Navigator(store);

            var result = navigator.FindEntity(id);

            //assert

            Assert.That(tomato, Is.EqualTo(result));
        }
        [Test]
        public void FindEntityAsNestedProp()
        {
            // arrange
            var tomato = new TomatoEntity();
            var id = tomato.Id;


            var store = new SalladStore();
            store.Bowl = new BowlEntity();
            store.Bowl.Tomato = tomato;

            // act
            var navigator = new Navigator(store);

            var result = navigator.FindEntity(id);

            //assert
            Assert.That(tomato, Is.EqualTo(result));
        }
    }
}