using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

namespace ComponentHotelCL
{
    public class DAL
    {
        // Database connection string
        private readonly string _connectionString =
          "Data Source=.;Initial Catalog=HotelDb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";


        // SQL Functies

        //Guests

        // GetAllGuests
        public List<Gast> GetAllGuests()
        {
            var gasten = new List<Gast>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT GastId, Email, Voornaam, Achternaam, Telefoonnummer, Straat, Huisnummer, Postcode, Woonplaats, Land, IBAN FROM Gasten";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        gasten.Add(new Gast
                        {
                            GastId = reader.GetInt32(0),
                            Email = reader.GetString(1),
                            Voornaam = reader.GetString(2),
                            Achternaam = reader.GetString(3),
                            Telefoonnummer = reader.GetString(4),
                            Straat = reader.GetString(5),
                            Huisnummer = reader.GetString(6),
                            Postcode = reader.GetString(7),
                            Woonplaats = reader.GetString(8),
                            Land = reader.GetString(9),
                            IBAN = reader.GetString(10)
                        });
                    }
                }
            }
            return gasten;
        }


        // ADD
        public void AddGuest(Gast gast)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Gasten (Email, Voornaam, Achternaam, Telefoonnummer, Straat, Huisnummer, Postcode, Woonplaats, Land, IBAN) VALUES (@Email, @Voornaam, @Achternaam, @Telefoonnummer, @Straat, @Huisnummer, @Postcode, @Woonplaats, @Land, @IBAN)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", gast.Email);
                    cmd.Parameters.AddWithValue("@Voornaam", gast.Voornaam);
                    cmd.Parameters.AddWithValue("@Achternaam", gast.Achternaam);
                    cmd.Parameters.AddWithValue("@Telefoonnummer", gast.Telefoonnummer);
                    cmd.Parameters.AddWithValue("@Straat", gast.Straat);
                    cmd.Parameters.AddWithValue("@Huisnummer", gast.Huisnummer);
                    cmd.Parameters.AddWithValue("@Postcode", gast.Postcode);
                    cmd.Parameters.AddWithValue("@Woonplaats", gast.Woonplaats);
                    cmd.Parameters.AddWithValue("@Land", gast.Land);
                    cmd.Parameters.AddWithValue("@IBAN", gast.IBAN);
                    cmd.ExecuteNonQuery();
                }

            }
        }


        // GET BY ID
        public Gast? GetGuestById(int gastid)
        {
            Gast? gast = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT GastId, Email, Voornaam, Achternaam, Telefoonnummer, Straat, Huisnummer, Postcode, Woonplaats, Land, IBAN FROM Gasten WHERE GastId = @GastId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@GastId", gastid);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            gast = new Gast
                            {
                                GastId = reader.GetInt32(0),
                                Email = reader.GetString(1),
                                Voornaam = reader.GetString(2),
                                Achternaam = reader.GetString(3),
                                Telefoonnummer = reader.GetString(4),
                                Straat = reader.GetString(5),
                                Huisnummer = reader.GetString(6),
                                Postcode = reader.GetString(7),
                                Woonplaats = reader.GetString(8),
                                Land = reader.GetString(9),
                                IBAN = reader.GetString(10)
                            };
                        }
                    }
                }
            }
            return gast;
        }


        // UPDATE
        public void UpdateGuest(Gast gast)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Gasten SET Email = @Email, Voornaam = @Voornaam, Achternaam = @Achternaam, Telefoonnummer = @Telefoonnummer, Straat = @Straat, Huisnummer = @Huisnummer, Postcode = @Postcode, Woonplaats = @Woonplaats, Land = @Land, IBAN = @IBAN WHERE GastId = @GastId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@GastId", gast.GastId);
                    cmd.Parameters.AddWithValue("@Email", gast.Email);
                    cmd.Parameters.AddWithValue("@Voornaam", gast.Voornaam);
                    cmd.Parameters.AddWithValue("@Achternaam", gast.Achternaam);
                    cmd.Parameters.AddWithValue("@Telefoonnummer", gast.Telefoonnummer);
                    cmd.Parameters.AddWithValue("@Straat", gast.Straat);
                    cmd.Parameters.AddWithValue("@Huisnummer", gast.Huisnummer);
                    cmd.Parameters.AddWithValue("@Postcode", gast.Postcode);
                    cmd.Parameters.AddWithValue("@Woonplaats", gast.Woonplaats);
                    cmd.Parameters.AddWithValue("@Land", gast.Land);
                    cmd.Parameters.AddWithValue("@IBAN", gast.IBAN);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        // DELETE
        public void DeleteGuest(int gastid)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Gasten WHERE GastId = @GastId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@GastId", gastid);
                    cmd.ExecuteNonQuery();
                }
            }
        }




        //===========================================
        //===========================================



        //Gebruiker

        //GetAllGebruikers
        public List<Gebruiker> GetAllUsers()
        {
            var gebruikers = new List<Gebruiker>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT g.GebruikerId, g.GastId, g.Email, g.WachtwoordHash, g.Rol, " +
                    "ga.GastId, ga.Email, ga.Voornaam, ga.Achternaam, ga.Telefoonnummer, ga.Straat, ga.Huisnummer, ga.Postcode, ga.Woonplaats, ga.Land, ga.IBAN " +
                    "FROM Gebruikers g JOIN Gasten ga ON g.GastId = ga.GastId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        gebruikers.Add(new Gebruiker
                        {
                            GebruikerId = reader.GetInt32(0),
                            GastId = reader.GetInt32(1),
                            Email = reader.GetString(2),
                            WachtwoordHash = reader.GetString(3),
                            Rol = reader.GetString(4),

                            Gast = new Gast
                            {
                                GastId = reader.GetInt32(5),
                                Email = reader.GetString(6),
                                Voornaam = reader.GetString(7),
                                Achternaam = reader.GetString(8),
                                Telefoonnummer = reader.GetString(9),
                                Straat = reader.GetString(10),
                                Huisnummer = reader.GetString(11),
                                Postcode = reader.GetString(12),
                                Woonplaats = reader.GetString(13),
                                Land = reader.GetString(14),
                                IBAN = reader.GetString(15)
                            }
                        });
                    }
                }
            }
            return gebruikers;
        }

        // ADD
        public void AddUser(Gebruiker gebruiker)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO Gebruikers (GastId, Email, WachtwoordHash, Rol) SELECT g.GastId, g.Email, @WachtwoordHash, @Rol FROM Gasten g WHERE g.GastId = @GastId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@GastId", gebruiker.GastId);
                    cmd.Parameters.AddWithValue("@WachtwoordHash", gebruiker.WachtwoordHash);
                    cmd.Parameters.AddWithValue("@Rol", gebruiker.Rol);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // GetByID
        public Gebruiker? GetUserById(int id)
        {
            Gebruiker? gebruiker = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT g.GebruikerId, g.GastId, g.Email, g.WachtwoordHash, g.Rol, " +
                    "ga.GastId, ga.Email, ga.Voornaam, ga.Achternaam, ga.Telefoonnummer, ga.Straat, ga.Huisnummer, ga.Postcode, ga.Woonplaats, ga.Land, ga.IBAN " + 
                    "FROM Gebruikers g JOIN Gasten ga ON g.GastId = ga.GastId WHERE GebruikerId = @GebruikerId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@GebruikerId", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            gebruiker = new Gebruiker
                            {
                                GebruikerId = reader.GetInt32(0),
                                GastId = reader.GetInt32(1),
                                Email = reader.GetString(2),
                                WachtwoordHash = reader.GetString(3),
                                Rol = reader.GetString(4),

                                Gast = new Gast
                                {
                                    GastId = reader.GetInt32(5),
                                    Email = reader.GetString(6),
                                    Voornaam = reader.GetString(7),
                                    Achternaam = reader.GetString(8),
                                    Telefoonnummer = reader.GetString(9),
                                    Straat = reader.GetString(10),
                                    Huisnummer = reader.GetString(11),
                                    Postcode = reader.GetString(12),
                                    Woonplaats = reader.GetString(13),
                                    Land = reader.GetString(14),
                                    IBAN = reader.GetString(15)
                                }
                            };
                        }
                    }
                }
            }
            return gebruiker;
        }

        // UPDATE
        public void UpdateUser(Gebruiker gebruiker)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Gebruikers SET GastId = @GastId, Email = @Email, WachtwoordHash = @WachtwoordHash, Rol = @Rol WHERE GebruikerId = @GebruikerId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@GebruikerId", gebruiker.GebruikerId);
                    cmd.Parameters.AddWithValue("@GastId", gebruiker.GastId);
                    cmd.Parameters.AddWithValue("@Email", gebruiker.Email);
                    cmd.Parameters.AddWithValue("@WachtwoordHash", gebruiker.WachtwoordHash);
                    cmd.Parameters.AddWithValue("@Rol", gebruiker.Rol);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // DELETE
        public void DeleteUser(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Gebruikers WHERE GebruikerId = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        //===========================================
        //===========================================

        // HotelKamer

        // GetAllHotelRooms
        public List<Hotelkamer> GetAllHotelRooms()
        {
            var hotelKamers = new List<Hotelkamer>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT HotelkamerId, Kamernummer, Type, PrijsPerNacht, IsBeschikbaar FROM HotelKamers";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        hotelKamers.Add(new Hotelkamer
                        {
                            HotelkamerId = reader.GetInt32(0),
                            Kamernummer = reader.GetString(1),
                            Type = reader.GetString(2),
                            PrijsPerNacht = reader.GetDecimal(3),
                            IsBeschikbaar = reader.GetBoolean(4)
                        });
                    }
                }
            }
            return hotelKamers;
        }


        // ADD
        public void AddHotelRoom(Hotelkamer hotelKamer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO HotelKamers (Kamernummer, Type, PrijsPerNacht, IsBeschikbaar) VALUES (@Kamernummer, @Type, @PrijsPerNacht, @IsBeschikbaar)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Kamernummer", hotelKamer.Kamernummer);
                    cmd.Parameters.AddWithValue("@Type", hotelKamer.Type);
                    cmd.Parameters.AddWithValue("@PrijsPerNacht", hotelKamer.PrijsPerNacht);
                    cmd.Parameters.AddWithValue("@IsBeschikbaar", hotelKamer.IsBeschikbaar);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // GetById
        public Hotelkamer? GetHotelRoomById(int id)
        {
            Hotelkamer? hotelKamer = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT HotelkamerId, Kamernummer, Type, PrijsPerNacht, IsBeschikbaar FROM HotelKamers WHERE HotelkamerId = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            hotelKamer = new Hotelkamer
                            {
                                HotelkamerId = reader.GetInt32(0),
                                Kamernummer = reader.GetString(1),
                                Type = reader.GetString(2),
                                PrijsPerNacht = reader.GetDecimal(3),
                                IsBeschikbaar = reader.GetBoolean(4)
                            };
                        }
                    }
                }
            }
            return hotelKamer;
        }

        // UPDATE
        public void UpdateHotelRoom(Hotelkamer hotelKamer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE HotelKamers SET Kamernummer = @Kamernummer, Type = @Type, PrijsPerNacht = @PrijsPerNacht, IsBeschikbaar = @IsBeschikbaar WHERE HotelkamerId = @HotelkamerId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@HotelkamerId", hotelKamer.HotelkamerId);
                    cmd.Parameters.AddWithValue("@Kamernummer", hotelKamer.Kamernummer);
                    cmd.Parameters.AddWithValue("@Type", hotelKamer.Type);
                    cmd.Parameters.AddWithValue("@PrijsPerNacht", hotelKamer.PrijsPerNacht);
                    cmd.Parameters.AddWithValue("@IsBeschikbaar", hotelKamer.IsBeschikbaar);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // DELETE
        public void DeleteHotelRoom(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM HotelKamers WHERE HotelkamerId = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }



        //===========================================
        //===========================================

        // Reservering(Reservations)

        // GetAllReservations
        public List<Reservering> GetAllReservations()
        {
            var reserveringen = new List<Reservering>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT r.ReserveringId, r.IncheckDatum, r.UitcheckDatum, r.Status, " +
                    "ga.GastId, ga.Email, ga.Voornaam, ga.Achternaam, hk.HotelkamerId, hk.Kamernummer, hk.Type, hk.PrijsPerNacht, " +
                    "t.TariefId, t.Subtotaal, t.KortingPercentage, t.BedragNaKorting, t.ToeristenbelastingPercentage, t.TotaalBedrag, t.BerekendOp " +
                    "FROM Reserveringen r " +
                    "JOIN Gasten ga ON r.GastId = ga.GastId " +
                    "LEFT JOIN ReserveringHotelkamers rhk ON r.ReserveringId = rhk.ReserveringId " +
                    "LEFT JOIN Hotelkamers hk ON rhk.HotelkamerId = hk.HotelkamerId " +
                    "LEFT JOIN Tarieven t ON r.ReserveringId = t.ReserveringId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reserveringen.Add(new Reservering
                        {
                            ReserveringId = reader.GetInt32(0),
                            IncheckDatum = reader.GetDateTime(1),
                            UitcheckDatum = reader.GetDateTime(2),
                            Status = reader.GetBoolean(3),

                            Gast = new Gast
                            {
                                GastId = reader.GetInt32(4),
                                Email = reader.GetString(5),
                                Voornaam = reader.GetString(6),
                                Achternaam = reader.GetString(7)
                            },

                            Hotelkamers = reader.IsDBNull(8) ? new List<ReserveringHotelkamer>() : new List<ReserveringHotelkamer>
                            {
                                new ReserveringHotelkamer
                                {
                                    Hotelkamer = new Hotelkamer
                                    {
                                        HotelkamerId = reader.GetInt32(8),
                                        Kamernummer = reader.GetString(9),
                                        Type = reader.GetString(10),
                                        PrijsPerNacht = reader.GetDecimal(11)
                                    }
                                }
                            },

                            Tarief = reader.IsDBNull(12) ? null : new Tarief
                            {
                                TariefId = reader.GetInt32(12),
                                Subtotaal = reader.GetDecimal(13),
                                KortingPercentage = reader.GetDecimal(14),
                                BedragNaKorting = reader.GetDecimal(15),
                                ToeristenbelastingPercentage = reader.GetDecimal(16),
                                TotaalBedrag = reader.GetDecimal(17),
                                BerekendOp = reader.GetDateTime(18)
                            }

                        });
                    }
                }
            }
            return reserveringen;
        }


        // ADD

        public void AddReservation(Reservering reservering)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        // 1️⃣ Insert Reservering met Status = TRUE
                        string queryReservering = @"
                            INSERT INTO Reserveringen (GastId, IncheckDatum, UitcheckDatum, Status) OUTPUT INSERTED.ReserveringId VALUES (@GastId, @IncheckDatum, @UitcheckDatum, 1)";

                        int newReserveringId;
                        using (SqlCommand cmd = new SqlCommand(queryReservering, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@GastId", reservering.GastId);
                            cmd.Parameters.AddWithValue("@IncheckDatum", reservering.IncheckDatum);
                            cmd.Parameters.AddWithValue("@UitcheckDatum", reservering.UitcheckDatum);

                            newReserveringId = (int)cmd.ExecuteScalar();
                        }

                        // 2️⃣ Insert Hotelkamers (één per rij)
                        string queryHotelkamer = @"
                    INSERT INTO ReserveringHotelkamers (ReserveringId, HotelkamerId)
                    VALUES (@ReserveringId, @HotelkamerId)";

                        foreach (var rhk in reservering.Hotelkamers)
                        {
                            using (SqlCommand cmd = new SqlCommand(queryHotelkamer, conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@ReserveringId", newReserveringId);
                                cmd.Parameters.AddWithValue("@HotelkamerId", rhk.Hotelkamer.HotelkamerId);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }


        // GetById
        public Reservering? GetReservationById(int id)
        {
            Reservering? reservering = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT ReserveringId, GastId, IncheckDatum, UitcheckDatum, TariefId, Status FROM Reserveringen WHERE ReserveringId = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            reservering = new Reservering
                            {
                                ReserveringId = reader.GetInt32(0),
                                GastId = reader.GetInt32(1),
                                IncheckDatum = reader.GetDateTime(2),
                                UitcheckDatum = reader.GetDateTime(3),
                                TariefId = reader.GetInt32(4),
                                Status = reader.GetBoolean(5)
                            };
                        }
                    }
                }
            }
            return reservering;
        }


        // UPDATE
        public void UpdateReservation(Reservering reservering)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Reserveringen SET GastId = @GastId, IncheckDatum = @IncheckDatum, UitcheckDatum = @UitcheckDatum, TariefId = @TariefId, Status = @Status WHERE ReserveringId = @ReserveringId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ReserveringId", reservering.ReserveringId);
                    cmd.Parameters.AddWithValue("@GastId", reservering.GastId);
                    cmd.Parameters.AddWithValue("@IncheckDatum", reservering.IncheckDatum);
                    cmd.Parameters.AddWithValue("@UitcheckDatum", reservering.UitcheckDatum);
                    cmd.Parameters.AddWithValue("@TariefId", reservering.TariefId);
                    cmd.Parameters.AddWithValue("@Status", reservering.Status);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        // DELETE
        public void DeleteReservation(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Reserveringen WHERE ReserveringId = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        //===========================================
        //===========================================

        // Bijwwerking nodig!
        // Tarief (Rates)

        // GetAllRates
        public List<Tarief> GetAllRates()
        {
            var tarieven = new List<Tarief>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT TariefId, ReserveringId, Subtotaal, KortingPercentage, BedragNaKorting, ToeristenbelastingPercentage, TotaalBedrag, BerekendOp FROM Tarieven";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tarieven.Add(new Tarief
                        {
                            TariefId = reader.GetInt32(0),
                            ReserveringId = reader.GetInt32(1),
                            Subtotaal = reader.GetDecimal(2),
                            KortingPercentage = reader.GetDecimal(3),
                            BedragNaKorting = reader.GetDecimal(4),
                            ToeristenbelastingPercentage = reader.GetDecimal(5),
                            TotaalBedrag = reader.GetDecimal(6),
                            BerekendOp = reader.GetDateTime(7)
                        });
                    }
                }
            }
            return tarieven;
        }

        // ADD
        public void AddRate(Tarief tarief)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Tarieven (ReserveringId, Subtotaal, KortingPercentage, BedragNaKorting, ToeristenbelastingPercentage, TotaalBedrag, BerekendOp) VALUES (@ReserveringId, @Subtotaal, @KortingPercentage, @BedragNaKorting, @ToeristenbelastingPercentage, @TotaalBedrag, @BerekendOp)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ReserveringId", tarief.ReserveringId);
                    cmd.Parameters.AddWithValue("@Subtotaal", tarief.Subtotaal);
                    cmd.Parameters.AddWithValue("@KortingPercentage", tarief.KortingPercentage);
                    cmd.Parameters.AddWithValue("@BedragNaKorting", tarief.BedragNaKorting);
                    cmd.Parameters.AddWithValue("@ToeristenbelastingPercentage", tarief.ToeristenbelastingPercentage);
                    cmd.Parameters.AddWithValue("@TotaalBedrag", tarief.TotaalBedrag);
                    cmd.Parameters.AddWithValue("@BerekendOp", tarief.BerekendOp);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // GetById
        public Tarief? GetRateById(int id)
        {
            Tarief? tarief = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT TariefId, ReserveringId, Subtotaal, KortingPercentage, BedragNaKorting, ToeristenbelastingPercentage, TotaalBedrag, BerekendOp FROM Tarieven WHERE TariefId = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tarief = new Tarief
                            {
                                TariefId = reader.GetInt32(0),
                                ReserveringId = reader.GetInt32(1),
                                Subtotaal = reader.GetDecimal(2),
                                KortingPercentage = reader.GetDecimal(3),
                                BedragNaKorting = reader.GetDecimal(4),
                                ToeristenbelastingPercentage = reader.GetDecimal(5),
                                TotaalBedrag = reader.GetDecimal(6),
                                BerekendOp = reader.GetDateTime(7)
                            };
                        }
                    }
                }
            }
            return tarief;
        }


        // UPDATE
        public void UpdateRate(Tarief tarief)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Tarieven SET ReserveringId = @ReserveringId, Subtotaal = @Subtotaal, KortingPercentage = @KortingPercentage, BedragNaKorting = @BedragNaKorting, ToeristenbelastingPercentage = @ToeristenbelastingPercentage, TotaalBedrag = @TotaalBedrag, BerekendOp = @BerekendOp WHERE TariefId = @TariefId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TariefId", tarief.TariefId);
                    cmd.Parameters.AddWithValue("@ReserveringId", tarief.ReserveringId);
                    cmd.Parameters.AddWithValue("@Subtotaal", tarief.Subtotaal);
                    cmd.Parameters.AddWithValue("@KortingPercentage", tarief.KortingPercentage);
                    cmd.Parameters.AddWithValue("@BedragNaKorting", tarief.BedragNaKorting);
                    cmd.Parameters.AddWithValue("@ToeristenbelastingPercentage", tarief.ToeristenbelastingPercentage);
                    cmd.Parameters.AddWithValue("@TotaalBedrag", tarief.TotaalBedrag);
                    cmd.Parameters.AddWithValue("@BerekendOp", tarief.BerekendOp);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        // DELETE
        public void DeleteRate(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Tarieven WHERE TariefId = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
