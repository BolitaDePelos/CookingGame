internal interface IFormControllable
{
    public IFormSubmitable FormSubmitable { get; set; }
    public void Submit();
}