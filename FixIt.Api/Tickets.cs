namespace FixIt.Api;

public class Tickets {
	public string User { get; set; }
	public string Description {get; set;}
	public int Level {get; set;}

	public Tickets(string user, string description, int level) {
		User = user;
		Description = description;
		Level = level;
	}
}