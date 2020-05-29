namespace FileShareSystem.Server {
    public interface Broker {
        byte[] ProcessData(byte[] data);
    }
}
