/// <summary>
/// The <c>IExportable</c> interface defines an interface for exporting and importing data about an object in a game scene
/// </summary>
public interface IExportable<T>
{
    /// <summary>
    /// Export the object data to a serializable object
    /// </summary>
    /// <returns>The exported serializable data object</returns>
    public T Export();

    /// <summary>
    /// Import the object data from the serializable object provided
    /// </summary>
    /// <param name="o">The serializable object containing data to be imported</param>
    public void Import(T o);
}