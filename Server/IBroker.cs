namespace FileShareSystem.Server {
    public interface IBroker {
        byte[] ProcessData(byte[] data);
    }
}
