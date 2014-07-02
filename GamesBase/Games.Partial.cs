namespace GamesBase
{
	partial class Games
	{
		public string GetFullName()
		{
			string name;
			if (OriginalGame != null)
				if (OriginalGame.OriginalGame != null)
					name = string.Format(OriginalGame.OriginalGame.Name.Contains(":") ? "{0} - {1} - {2}" : "{0}: {1} - {2}",
						OriginalGame.OriginalGame.Name, OriginalGame.Name, Name);
				else
					name = string.Format(OriginalGame.Name.Contains(":") ? "{0} - {1}" : "{0}: {1}", OriginalGame.Name, Name);
			else
				name = Name;
			return name;
		}

		public string GetContentName()
		{
			return OriginalGame.OriginalGame != null ? string.Format("{0} - {1}", OriginalGame.Name, Name) : Name;
		}
	}
}
