using Microsoft.EntityFrameworkCore;
using SatMonitor.Domain.Entities;
using SatMonitor.Domain.Enums;

namespace SatMonitor.Infrastructure.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Missoes.CountAsync() > 0) return;

        var missao1 = new Missao
        {
            Nome = "Sentinel Earth-1",
            Descricao = "Monitoramento climático e ambiental da superfície terrestre",
            DataLancamento = new DateTime(2023, 3, 15),
            Status = StatusMissao.Ativa
        };

        var missao2 = new Missao
        {
            Nome = "OrbitalWatch BR",
            Descricao = "Vigilância orbital e detecção de detritos espaciais",
            DataLancamento = new DateTime(2024, 7, 1),
            Status = StatusMissao.Ativa
        };

        var missao3 = new Missao
        {
            Nome = "AgroSat Prime",
            Descricao = "Monitoramento agrícola e de recursos hídricos",
            DataLancamento = new DateTime(2022, 11, 20),
            Status = StatusMissao.Encerrada
        };

        await context.Missoes.AddRangeAsync(missao1, missao2, missao3);
        await context.SaveChangesAsync();

        var sat1 = new Satelite
        {
            Nome = "SAT-BR-01",
            Altitude = 550.5,
            Inclinacao = 97.6,
            DataLancamento = new DateTime(2023, 4, 10),
            MissaoId = missao1.Id
        };

        var sat2 = new Satelite
        {
            Nome = "SAT-BR-02",
            Altitude = 620.0,
            Inclinacao = 53.0,
            DataLancamento = new DateTime(2023, 9, 5),
            MissaoId = missao1.Id
        };

        var sat3 = new Satelite
        {
            Nome = "OW-ALPHA",
            Altitude = 800.0,
            Inclinacao = 98.2,
            DataLancamento = new DateTime(2024, 8, 12),
            MissaoId = missao2.Id
        };

        await context.Satelites.AddRangeAsync(sat1, sat2, sat3);
        await context.SaveChangesAsync();

        var sensor1 = new Sensor
        {
            Nome = "Sensor Térmico Principal",
            Tipo = TipoSensor.Temperatura,
            Unidade = "°C",
            LimiteMin = -80.0,
            LimiteMax = 120.0,
            SateliteId = sat1.Id
        };

        var sensor2 = new Sensor
        {
            Nome = "Sensor de Pressão Atmosférica",
            Tipo = TipoSensor.Pressao,
            Unidade = "hPa",
            LimiteMin = 0.001,
            LimiteMax = 1013.0,
            SateliteId = sat1.Id
        };

        var sensor3 = new Sensor
        {
            Nome = "Detector de Radiação Solar",
            Tipo = TipoSensor.Radiacao,
            Unidade = "W/m²",
            LimiteMin = 0.0,
            LimiteMax = 1400.0,
            SateliteId = sat2.Id
        };

        var sensor4 = new Sensor
        {
            Nome = "Magnetômetro Orbital",
            Tipo = TipoSensor.Magnetometro,
            Unidade = "nT",
            LimiteMin = 20000.0,
            LimiteMax = 65000.0,
            SateliteId = sat3.Id
        };

        await context.Sensores.AddRangeAsync(sensor1, sensor2, sensor3, sensor4);
        await context.SaveChangesAsync();

        var leituras = new List<LeituraSensor>();
        var random = new Random(42);
        var agora = DateTime.Now;

        var leiturasSensor1 = new[]
        {
            (-45.2, -6), (-12.5, -5), (25.8, -4), (78.3, -3),
            (95.1, -2), (130.0, -1), (-90.0, 0)
        };

        foreach (var (valor, horasAtras) in leiturasSensor1)
        {
            leituras.Add(new LeituraSensor
            {
                Valor = valor,
                DataHoraLeitura = agora.AddHours(horasAtras),
                SensorId = sensor1.Id,
                Status = sensor1.CalcularStatus(valor)
            });
        }

        for (int i = 0; i < 8; i++)
        {
            var valor = 0.001 + random.NextDouble() * 900;
            leituras.Add(new LeituraSensor
            {
                Valor = Math.Round(valor, 3),
                DataHoraLeitura = agora.AddHours(-i),
                SensorId = sensor2.Id,
                Status = sensor2.CalcularStatus(valor)
            });
        }

        for (int i = 0; i < 6; i++)
        {
            var valor = random.NextDouble() * 1600;
            leituras.Add(new LeituraSensor
            {
                Valor = Math.Round(valor, 2),
                DataHoraLeitura = agora.AddHours(-i),
                SensorId = sensor3.Id,
                Status = sensor3.CalcularStatus(valor)
            });
        }

        for (int i = 0; i < 5; i++)
        {
            var valor = 15000 + random.NextDouble() * 55000;
            leituras.Add(new LeituraSensor
            {
                Valor = Math.Round(valor, 1),
                DataHoraLeitura = agora.AddHours(-i),
                SensorId = sensor4.Id,
                Status = sensor4.CalcularStatus(valor)
            });
        }

        await context.Leituras.AddRangeAsync(leituras);
        await context.SaveChangesAsync();
    }
}