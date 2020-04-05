namespace PPO2._3
{
    public class Test
    {
        public static void test()
        {
            Manager.AddUser("Ivan");
            Manager.AddUser("Alexander");
            EventHandler.OnEnter(1);
            EventHandler.OnEnter(2);
            EventHandler.onExit(1);
            EventHandler.onExit(2);
            
            EventHandler.OnEnter(1);
            EventHandler.onExit(1);
            
            EventHandler.OnEnter(2);
            
            StatiscsHandler.RebootStatistics();

            Manager.GetInfo(1);
            Manager.GetInfo(2);
            
            Manager.Prolongate(1, 10);
            Manager.GetInfo(1);
            
        }
    }
}