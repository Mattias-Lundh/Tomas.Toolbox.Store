// See https://aka.ms/new-console-template for more information


using NUnit.Framework;
using Store_tests;
using Toolbox.Store;
using Toolbox.Store.Components;

//// COMPARER
// arrange


// act

//var result = comparer.EqualTo(tomato1, tomato2);
//var negative = comparer.EqualTo(tomato1, tomato3);



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


var result = comparer.EqualTo(table1, table3);

//assert
Console.WriteLine(result);


//// NAVIGATOR
//// arrange

//var tomato = new TomatoEntity();
//var id = tomato.Id;

//var table = new TableEntity();
//table.Bowl = new BowlEntity();
//table.Bowl.Tomato = tomato;

//var store = new SalladStore();
//store.Tables.Add(table);

//// act
//var navigator = new Navigator(store);

//var result = navigator.FindEntity(id);

//Console.WriteLine(result);
