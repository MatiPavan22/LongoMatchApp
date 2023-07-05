using System;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;

namespace LongoToDo.UITest
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        readonly Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void TodoItemsPageIsDisplayedOK()
        {
            //Arrange
            var todoItemsListView = app.Query(e => e.Marked("listViewTodoItems")).Any();
            var btnAdd = app.Query(e => e.Marked("AddButton")).Any();
            var lengthTodoItems = app.Query(e => e.Marked("listViewTodoItems").Child()).Length;

            //Assert
            Assert.IsTrue(todoItemsListView);
            Assert.IsTrue(btnAdd);
            Assert.IsTrue(lengthTodoItems >= 1);
        }

        [Test]
        public void ButtonAddClicked()
        {
            //Arrange & Act
            app.Tap("AddButton");

            var entryName = app.Query(e => e.Marked("txt_name")).Any();
            var btnSave = app.Query(e => e.Marked("btn_saveTodoItem")).Any();

            //Assert
            Assert.IsTrue(entryName);
            Assert.IsTrue(btnSave);
        }

        [Test]
        public void AddTodoItemAndRefreshListView()
        {
            //Arrange
            Random random = new Random();
            var lengthInitialTodoItems = app.Query(e => e.Marked("listViewTodoItems").Child()).Length;
            app.Tap("AddButton");
            app.Tap("txt_name");
            app.EnterText($"Item {random.Next(1, 10)}");
            app.DismissKeyboard();
            
            //Act
            app.Tap("btn_saveTodoItem");
            app.WaitForElement("listViewTodoItems");

            var lengthTodoItemsAfterAdded = app.Query(e => e.Marked("listViewTodoItems").Child()).Length;

            //Assert
            Assert.IsTrue(lengthTodoItemsAfterAdded > lengthInitialTodoItems);
        }

        [Test]
        public void ButtonAddClickedAndBackToTodoItemList()
        {
            //Arrange
            var lengthInitialTodoItems = app.Query(e => e.Marked("listViewTodoItems").Child()).Length;

            //Act
            app.Tap("AddButton");
            app.Back();

            app.WaitForElement("listViewTodoItems");

            var lengthTodoItemsAfterBack = app.Query(e => e.Marked("listViewTodoItems").Child()).Length;

            //Assert
            Assert.IsTrue(lengthTodoItemsAfterBack == lengthInitialTodoItems);
        }

        [Test]
        public void TodoItemMarkedCompleteOrNotComplete()
        {
            //Arrange
            bool todoItemIsComplete = (bool)app.Query(x => x.Marked("cb_IsComplete").Invoke("isChecked"))?.FirstOrDefault();

            //Act
            app.Tap(c => c.Marked("cb_IsComplete").Index(0));

            //Assert
            bool checkboxValue_afterChecked = (bool)app.Query(x => x.Marked("cb_IsComplete").Invoke("isChecked"))?.FirstOrDefault();
            Assert.IsTrue(todoItemIsComplete != checkboxValue_afterChecked);
        }

        [Test]
        public void TodoItemDeleted()
        {
            //Arrange
            var lengthInitialTodoItems = app.Query(e => e.Marked("listViewTodoItems").Child()).Length;

            //Act
            app.TouchAndHold(c => c.Class("ViewCellRenderer_ViewCellContainer").Index(0));
            app.Tap(c => c.Text("Delete"));
            app.Tap(c => c.Text("Yes"));
            app.WaitForElement("listViewTodoItems");

            //Assert
            var lengthTodoItemsAfterDeleted = app.Query(e => e.Marked("listViewTodoItems").Child()).Length;
            Assert.IsTrue(lengthTodoItemsAfterDeleted < lengthInitialTodoItems);
        }

        [Test]
        public void TodoItemNotDeleted()
        {
            //Arrange
            var lengthInitialTodoItems = app.Query(e => e.Marked("listViewTodoItems").Child()).Length;

            //Act
            app.TouchAndHold(c => c.Class("ViewCellRenderer_ViewCellContainer").Index(0));
            app.Tap(c => c.Text("Delete"));            
            app.Tap(c => c.Text("No"));
            app.WaitForElement("listViewTodoItems");

            //Assert
            var lengthTodoItemsAfterNotDeleted = app.Query(e => e.Marked("listViewTodoItems").Child()).Length;
            Assert.IsTrue(lengthTodoItemsAfterNotDeleted == lengthInitialTodoItems);
        }

        [Test]
        public void ListViewRefresh()
        {
            //Arrange
            var listViewRect = app.Query(c => c.Marked("listViewTodoItems")).First().Rect;

            //Act
            app.DragCoordinates(listViewRect.CenterX, listViewRect.Y + 10, listViewRect.CenterX, listViewRect.Y + 410);
            app.WaitForElement(c => c.Marked("listViewTodoItems"), timeout: TimeSpan.FromSeconds(10));            

            //Assert
            var lengthTodoItems = app.Query(e => e.Marked("listViewTodoItems").Child()).Length;
            Assert.IsTrue(lengthTodoItems > 0);
        }

        //[Test]
        //public void OpenRepl()
        //{
        //    app.Repl();
        //}
    }
}
