//this model is used to return the record to client, include the image source, then display the image in the view page.
public class ReturnData
{
    public string Name { get; set; }
    public string ImageBase64 { get; set; }
}