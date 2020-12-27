using System.Drawing;

namespace LeagueBot.Constants {
    /// <summary>
    /// Pour que le BOT fonctionne, l'écran de l'ordinateur doit posséder une résolution de 1920 * 1080
    /// </summary>
    /// 
    public static class PixelsConstants {
        public static Point GAME_RESOLUTION = new Point(1024, 768); // fuck morten

        #region Launcher
        public static Point PLAY_BUTTON = new Point(317, 140).Resize(GAME_RESOLUTION);// fixed

        public static Point COOP_AGAINST_AI_MBUTTON = new Point(354, 213).Resize(GAME_RESOLUTION); // fixed
        public static Point PVP_MBUTTON = new Point(242, 209).Resize(GAME_RESOLUTION);
        public static Point TRAINING_MBUTTON = new Point(456, 214).Resize(GAME_RESOLUTION); //fixed
        public static Point TFT_EVENT = new Point(1251, 345).Resize(GAME_RESOLUTION);
        public static Point TFT_NO_EVENT = new Point(1098, 341).Resize(GAME_RESOLUTION);

        public static Point COOP_AGAINST_AI_BEGGINER = new Point(682, 745).Resize(GAME_RESOLUTION); //fixed
        public static Point COOP_AGAINST_AI_INTERMEDIATE = new Point(470, 631).Resize(GAME_RESOLUTION);
        public static Point PRACTICE_TOOL_BUTTON = new Point(943, 361).Resize(GAME_RESOLUTION); //fixed

        public static Point ARAM_BUTTON = new Point(737, 309).Resize(GAME_RESOLUTION); //why tho

        public static Point CONFIRM_BUTTON = new Point(811, 949).Resize(GAME_RESOLUTION);   //fixed
        public static Point ACCEPT_MATCH_BUTTON = new Point(962, 759).Resize(GAME_RESOLUTION);  //fixed
        public static Point DECLINE_MATCH_BUTTON = new Point(954, 868).Resize(GAME_RESOLUTION); //fixed
        public static Point QUE_TEST_POINT = new Point(1581, 160).Resize(GAME_RESOLUTION);
        public static Point ACCEPT_LOOT = new Point(1036, 919).Resize(GAME_RESOLUTION);
        public static Point STUCK_ON_RECONECT = new Point(894, 520).Resize(GAME_RESOLUTION);

        public static Point LEAVE_BUTTON = new Point(716, 946).Resize(GAME_RESOLUTION);
        public static Point HONOR_BUTTON = new Point(600, 600).Resize(GAME_RESOLUTION);
        public static Point LEVELUP_BUTTON = new Point(955, 937).Resize(GAME_RESOLUTION);

        public static Point[] CHECK_PLACE_TFT = {
            new Point(207, 255).Resize(GAME_RESOLUTION),
            new Point(207, 314).Resize(GAME_RESOLUTION),
            new Point(207, 373).Resize(GAME_RESOLUTION),
            new Point(207, 433).Resize(GAME_RESOLUTION),
            new Point(207, 494).Resize(GAME_RESOLUTION),
            new Point(207, 549).Resize(GAME_RESOLUTION),
            new Point(207, 608).Resize(GAME_RESOLUTION),
            new Point(207, 667).Resize(GAME_RESOLUTION)
        };

        #endregion

        #region Champion Select
        public static Point CHAMP1_LOGO = new Point(638, 290).Resize(GAME_RESOLUTION);//fixed
        public static Point CHAMP2_LOGO = new Point(775, 300).Resize(GAME_RESOLUTION);//fixed
        public static Point LOCK_BUTTON = new Point(941, 849).Resize(GAME_RESOLUTION);//fixed

        public static Point CHOOSE_YOUR_LOADOUT_TEXT = new Point(1165, 123).Resize(GAME_RESOLUTION); // no need???
        #endregion

        #region Game
        public static Point SUMMONER_1_SLOT = new Point(1008, 856).Resize(GAME_RESOLUTION);
        public static Point SUMMONER_2_SLOT = new Point(1047, 856).Resize(GAME_RESOLUTION);

        public static Point Q_SLOT = new Point(824, 856).Resize(GAME_RESOLUTION);
        public static Point Z_SLOT = new Point(867, 856).Resize(GAME_RESOLUTION);
        public static Point E_SLOT = new Point(912, 856).Resize(GAME_RESOLUTION);
        public static Point R_SLOT = new Point(966, 856).Resize(GAME_RESOLUTION);

        public static Point UP_Q_SLOT = new Point(821, 821).Resize(GAME_RESOLUTION);
        public static Point UP_Z_SLOT = new Point(862, 817).Resize(GAME_RESOLUTION);
        public static Point UP_E_SLOT = new Point(912, 818).Resize(GAME_RESOLUTION);
        public static Point UP_R_SLOT = new Point(961, 816).Resize(GAME_RESOLUTION);

        public static Point SHOP_BUTTON = new Point(1157, 923).Resize(GAME_RESOLUTION);
        public static Point SHOP_ITEM1 = new Point(504, 324).Resize(GAME_RESOLUTION);
        public static Point SHOP_ITEM2 = new Point(780, 318).Resize(GAME_RESOLUTION);

        public static Point LOCK_CAMERA_BUTTON = new Point(1242, 926).Resize(GAME_RESOLUTION);
        public static Point LOCK_CAMERA_TEST = new Point(1239, 899).Resize(GAME_RESOLUTION);
        public static Point CLOSE_SHOP_BUTTON = new Point(1432, 190).Resize(GAME_RESOLUTION);

        public static Point BACK_BUTTON = new Point(1197, 894).Resize(GAME_RESOLUTION);

        public static Point MINIMAP_TOPLEFT_POINT = new Point(1290, 756).Resize(GAME_RESOLUTION);
        public static Point MINIMAP_BOTTOMRIGHT_POINT = new Point(1463, 930).Resize(GAME_RESOLUTION);

        public static Point LIFE_BAR_CHECKER_POINT = new Point(1016, 891).Resize(GAME_RESOLUTION); //fixed
        public static Point LIFE_BAR_CHECKER_POINT_LOW_HEALTH = new Point(833, 910).Resize(GAME_RESOLUTION); //TODO

        public static Point GAME_MOVE_TEST_01 = new Point(500, 900).Resize(GAME_RESOLUTION);
        public static Point GAME_MOVE_TEST_02 = new Point(1451, 209).Resize(GAME_RESOLUTION);
        public static Point GAME_MOVE_TEST_03 = new Point(707, 187).Resize(GAME_RESOLUTION);
        public static Point GAME_MOVE_TEST_04 = new Point(1245, 886).Resize(GAME_RESOLUTION);
        #endregion

        #region Summoner's Rift

        #region Blue side
        public static Point BLUESIDE_RED = new Point(1377, 878).Resize(GAME_RESOLUTION);
        public static Point BLUESIDE_WOLFS = new Point(1329, 849).Resize(GAME_RESOLUTION);
        public static Point BLUESIDE_GROMP = new Point(1308, 828).Resize(GAME_RESOLUTION);

        public static Point MIDLANE_MID = new Point(1474, 838).Resize(GAME_RESOLUTION);
        public static Point BOTLANE_BOT = new Point(1438, 899).Resize(GAME_RESOLUTION);

        public static Point BLUESIDE_BOTLANE_T1 = new Point(1414, 917).Resize(GAME_RESOLUTION);
        public static Point REDSITE_BOTLANE_T1 = new Point(1454, 847).Resize(GAME_RESOLUTION);

        public static Point BLUE_FARM_T1_1 = new Point(1417, 908).Resize(GAME_RESOLUTION);
        public static Point BLUE_FARM_T1_2 = new Point(1417, 915).Resize(GAME_RESOLUTION);

        public static Point BLUE_BOTLANE_T1_SAFE = new Point(1406, 916).Resize(GAME_RESOLUTION);
        #endregion

        #endregion

        #region Howling Abyss
        /// <summary>
        /// RIP
        /// </summary>
        public static Point HOWLING_ABYSS_MID = new Point(1373, 842).Resize(GAME_RESOLUTION);
        public static Point HOWLING_ABYSS_BLUE_T1 = new Point(1355, 833).Resize(GAME_RESOLUTION);
        public static Point HOWLING_ABYSS_RED_T1 = new Point(1394, 793).Resize(GAME_RESOLUTION);
        public static Point HOWLING_ABYSS_BUSH1 = new Point(1356, 842).Resize(GAME_RESOLUTION);
        public static Point HOWLING_ABYSS_BUSH2 = new Point(1375, 804).Resize(GAME_RESOLUTION);

        public static Point HOWLING_ABYSS_REDNEXUS = new Point(1438, 775).Resize(GAME_RESOLUTION);
        public static Point HOWLING_ABYSS_BLUENEXUS = new Point(1309, 903).Resize(GAME_RESOLUTION);
        #endregion

        #region TFT

        public static Point TFT_MAPBORDER = new Point(1345, 914).Resize(GAME_RESOLUTION);
        public static Point SHOP_BORDER = new Point(1197, 819).Resize(GAME_RESOLUTION);

        public static Point TFT_SURRENDER = new Point(784, 862).Resize(GAME_RESOLUTION);
        public static Point TFT_SURRENDER2 = new Point(917, 593).Resize(GAME_RESOLUTION);
        public static Point DEAT_EXIT_BUTTON = new Point(865, 559).Resize(GAME_RESOLUTION);

        public static Point[] BUY_UNIT = {
            new Point(693, 871).Resize(GAME_RESOLUTION),
            new Point(828, 871).Resize(GAME_RESOLUTION),
            new Point(975, 871).Resize(GAME_RESOLUTION),
            new Point(1136, 871).Resize(GAME_RESOLUTION),
            new Point(1269, 871).Resize(GAME_RESOLUTION)
        };

        // TODO: remove
        //public static Point BUY_UNIT_1 = new Point(693, 871).Resize(GAME_RESOLUTION);
        //public static Point BUY_UNIT_2 = new Point(828, 871).Resize(GAME_RESOLUTION);
        //public static Point BUY_UNIT_3 = new Point(975, 871).Resize(GAME_RESOLUTION);
        //public static Point BUY_UNIT_4 = new Point(1136, 871).Resize(GAME_RESOLUTION);
        //public static Point BUY_UNIT_5 = new Point(1269, 871).Resize(GAME_RESOLUTION);

        #endregion


        public static Point Resize(this Point point, Point resolutionScale) {
            int xScale = resolutionScale.X / GAME_RESOLUTION.X;
            int YScale = resolutionScale.Y / GAME_RESOLUTION.Y;
            return new Point(point.X * xScale, point.Y * YScale);
        }
    }
}
