using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Store.Components;

namespace Store_tests.comparer
{
    public class ComparerTests
    {
        [Test]
        public void ComparesObjectWithSimpleProperties()
        {
            // arrange

            var tomato1 = new TomatoEntity();
            tomato1.Size = 4;

            var tomato2 = new TomatoEntity();
            tomato2.Size = 4;

            var tomato3 = new TomatoEntity();
            tomato3.Size = 666;

            var comparer = new Comparer();

            // act

            var result = comparer.EqualTo(tomato1, tomato2);
            var negative = comparer.EqualTo(tomato1, tomato3);
            //assert
            Assert.That(result, Is.EqualTo(true));
            Assert.That(negative, Is.Not.EqualTo(true));
        }


        [Test]
        public void ComparesObjectWithEntitiesAsProperties()
        {
            // arrange
            var tomato1 = new TomatoEntity();
            tomato1.Size = 4;

            var tomato2 = new TomatoEntity();
            tomato2.Size = 4;

            var tomato3 = new TomatoEntity();
            tomato3.Size = 666;

            var bowl1 = new BowlEntity();
            bowl1.Tomato = tomato1;

            var bowl2 = new BowlEntity();
            bowl2.Tomato = tomato2;

            var bowl3 = new BowlEntity();
            bowl3.Tomato = tomato3;

            var comparer = new Comparer();

            // act
            
            var result = comparer.EqualTo(bowl1, bowl2);
            var negative = comparer.EqualTo(bowl1, bowl3);

            //assert
            Assert.That(result, Is.EqualTo(true));
            Assert.That(negative, Is.Not.EqualTo(true));
        }

        [Test]
        public void ComparesObjectWithCollectionsAsProperties()
        {
            // arrange
            var tomato1 = new TomatoEntity();
            tomato1.Size = 4;

            var tomato2 = new TomatoEntity();
            tomato2.Size = 4;

            var bowl1 = new BowlEntity();
            bowl1.Tomato = tomato1;

            var bowl2 = new BowlEntity();
            bowl2.Tomato = tomato2;

            var table1 = new TableEntity();
            table1.Reserves.Add(bowl1);

            var table2 = new TableEntity();
            table2.Reserves.Add(bowl2);

            var table3 = new TableEntity();
            table3.Reserves.Add(bowl1);
            table3.Reserves.Add(bowl2);

            var comparer = new Comparer();

            // act

            var result = comparer.EqualTo(table1, table2);
            var negative = comparer.EqualTo(table1, table3);

            //assert
            Assert.That(result, Is.EqualTo(true));
            Assert.That(negative, Is.Not.EqualTo(true));
        }
    }
}