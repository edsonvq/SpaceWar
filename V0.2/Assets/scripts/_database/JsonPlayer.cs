[System.Serializable]
public struct JsonPlayer
{
	[System.Serializable]
	public struct Player
	{
		public int id;
		public string login;
        public string nick;
        public string password;
		public int score;
	}

	public Player[] data;
}