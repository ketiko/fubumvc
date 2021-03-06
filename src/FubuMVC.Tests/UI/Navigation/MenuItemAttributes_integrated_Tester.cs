using System.Diagnostics;
using FubuMVC.Core.Registration;
using FubuMVC.Core.UI.Navigation;
using NUnit.Framework;
using System.Linq;
using FubuTestingSupport;
using System.Collections.Generic;

namespace FubuMVC.Tests.UI.Navigation
{
    [TestFixture]
    public class MenuItemAttributes_integrated_Tester
    {
        private BehaviorGraph graph;

        [SetUp]
        public void SetUp()
        {
            graph = BehaviorGraph.BuildFrom(x => x.Actions.IncludeType<Controller1>());
        }

        [Test]
        public void puts_the_navigation_graph_in_the_right_order()
        {
            var navigationGraph = graph.Navigation;

            navigationGraph.FindNode(new NavigationKey("Two")).ShouldBeOfType<MenuNode>().Previous.Key.Key.ShouldEqual("Three");

            navigationGraph.MenuFor("Root").AllNodes().Select(x => x.Key.Key)
                .ShouldHaveTheSameElementsAs("Three", "Two", "One","Four");
        }

        public class Controller1
        {
            [MenuItem("One", AddChildTo = "Two")]
            public void One(Input1 input){}

            [MenuItem("Two", AddChildTo = "Root")]
            public void Two(Input1 input){}

            [MenuItem("Three", AddBefore = "Two")]
            public void Three(Input1 input){}

            [MenuItem("Four", AddChildTo = "Root")]
            public void Four(Input1 input) { }


            public class Input1 { }
        }
    }

    
}