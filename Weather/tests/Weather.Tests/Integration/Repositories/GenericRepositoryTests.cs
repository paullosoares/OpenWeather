using System;
using System.Linq;
using System.Threading.Tasks;
using Weather.Core.Data;
using Weather.Domain.Models;
using Weather.Tests.Integration.Collections;
using Xunit;

namespace Weather.Tests.Integration.Repositories
{
    [Collection(nameof(WeatherCollection))]
    public class GenericRepositoryTests : TestIntegrationBase
    {
        private readonly IRepository<OpenWeather> _repository;
        private readonly WeatherTestsFixture _weatherTestsFixture;

        public GenericRepositoryTests(WeatherTestsFixture weatherTestsFixture)
        {
            _weatherTestsFixture = weatherTestsFixture;
            CreateScope();
            _repository = GetInstance<IRepository<OpenWeather>>();
        }

        [Fact]
        public async Task Generic_AddEntity_MustBeSuccess()
        {
            //Arrange
            //Arrange
            var openWeather = _weatherTestsFixture.GenerateValidWeather();
            var coordinate = _weatherTestsFixture.GenerateValidCoordinate();
            var weatherMain = _weatherTestsFixture.GeneratevalidOpenWeatherMain();

            //Act
            openWeather.AddCoordinate(coordinate);
            openWeather.AddWeatherMain(weatherMain);

            //Act
            await _repository.AddAsync(openWeather);
            var success = await _repository.UnitOfWork.Commit();
            var item = await _repository.GetByIdAsync(openWeather.Id);

            //Assert
            Assert.True(success);
            Assert.NotNull(item);
            Assert.NotEqual(Guid.Empty, item.Id);
        }

        [Fact]
        public async Task Generic_UpdatedEntity_MustBeSuccess()
        {
            //Arrange
            var openWeather = _weatherTestsFixture.GenerateValidWeather();
            var coordinate = _weatherTestsFixture.GenerateValidCoordinate();
            var weatherMain = _weatherTestsFixture.GeneratevalidOpenWeatherMain();

            openWeather.AddCoordinate(coordinate);
            openWeather.AddWeatherMain(weatherMain);

            //Act
            await _repository.AddAsync(openWeather);
            var success = await _repository.UnitOfWork.Commit();

            var item = await _repository.GetByIdAsync(openWeather.Id);
            item.UpdateName("CityNewName");

            _repository.Update(item);
            var updateSuccess = await _repository.UnitOfWork.Commit();


            //Assert
            Assert.NotNull(item);
            Assert.True(success);
            Assert.True(updateSuccess);
        }

        [Fact]
        public async Task Generic_FindById_MustBeReturnEntity()
        {
            var openWeather = _weatherTestsFixture.GenerateValidWeather();
            var coordinate = _weatherTestsFixture.GenerateValidCoordinate();
            var weatherMain = _weatherTestsFixture.GeneratevalidOpenWeatherMain();

            openWeather.AddCoordinate(coordinate);
            openWeather.AddWeatherMain(weatherMain);

            //Act
            await _repository.AddAsync(openWeather);

            var success = await _repository.UnitOfWork.Commit();

            var item = await _repository.GetByIdAsync(openWeather.Id);

            //Assert
            Assert.True(success);
            Assert.NotNull(item);
            Assert.NotEqual(Guid.Empty, item.Id);
        }

        [Fact]
        public async Task Generic_RemoveEntity_MustBeSuccess()
        {
            var openWeather = _weatherTestsFixture.GenerateValidWeather();
            var coordinate = _weatherTestsFixture.GenerateValidCoordinate();
            var weatherMain = _weatherTestsFixture.GeneratevalidOpenWeatherMain();

            openWeather.AddCoordinate(coordinate);
            openWeather.AddWeatherMain(weatherMain);

            //Act
            await _repository.AddAsync(openWeather);

            var success = await _repository.UnitOfWork.Commit();

            var item = await _repository.GetByIdAsync(openWeather.Id);

            _repository.Remove(item);
            var removedSuccess = await _repository.UnitOfWork.Commit();

            var removedItem = await _repository.GetByIdAsync(item.Id);

            //Assert
            Assert.True(success);
            Assert.NotNull(item);
            Assert.NotEqual(Guid.Empty, item.Id);
            Assert.True(removedSuccess);
            Assert.Null(removedItem);
        }

        [Fact]
        public async Task Generic_GetByExpression_MustBeReturnEntities()
        {
            var openWeather = _weatherTestsFixture.GenerateValidWeather();
            var coordinate = _weatherTestsFixture.GenerateValidCoordinate();
            var weatherMain = _weatherTestsFixture.GeneratevalidOpenWeatherMain();

            openWeather.AddCoordinate(coordinate);
            openWeather.AddWeatherMain(weatherMain);

            //Act
            await _repository.AddAsync(openWeather);

            var success = await _repository.UnitOfWork.Commit();

            var items = await _repository.GetAll(m => m.Id == openWeather.Id);

            //Assert
            Assert.True(success);
            Assert.Single(items);
        }
    }
}
