namespace PrimeiraAPI.Model {
    public interface IEmplyeeRepository {
        void Add(Employee employee);

        List<Employee> Get();
    }
}
