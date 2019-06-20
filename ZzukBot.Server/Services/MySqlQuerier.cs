using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;
using ZzukBot.Server.Services.Interfaces;
using ZzukBot.Server.Services.Options;

namespace ZzukBot.Server.Services
{
    internal class MySqlQuerier : IMySqlQuerier
    {
        MySqlQuerierOptions Options { get; }

        public MySqlQuerier(IOptions<MySqlQuerierOptions> options)
        {
            Options = options.Value;
        }

        public bool FindEmailConfirmedByEmail(string email)
        {
            string sql = "SELECT EmailConfirmed FROM AspNetUsers WHERE NormalizedEmail = @email";

            using (MySqlConnection conn = new MySqlConnection(Options.DefaultConnection))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    var paramEmail = new MySqlParameter("email", MySqlDbType.VarChar)
                    {
                        Value = email.Normalize()
                    };

                    cmd.Parameters.Add(paramEmail);

                    var result = Convert.ToInt32(cmd.ExecuteScalar());

                    conn.Close();

                    return result == 1;
                }
            }
        }

        public async Task<bool> FindEmailConfirmedByEmailAsync(string email)
        {
            return await Task.Run(async () =>
            {
                string sql = "SELECT EmailConfirmed FROM AspNetUsers WHERE NormalizedEmail = @email";

                using (MySqlConnection conn = new MySqlConnection(Options.DefaultConnection))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        var paramEmail = new MySqlParameter("email", MySqlDbType.VarChar)
                        {
                            Value = email.Normalize()
                        };

                        cmd.Parameters.Add(paramEmail);

                        var result = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                        conn.Close();

                        return result == 1;
                    }
                }
            });
        }

        public string FindIdByEmail(string email)
        {
            string sql = "SELECT Id FROM AspNetUsers WHERE NormalizedEmail = @email";

            using (MySqlConnection conn = new MySqlConnection(Options.DefaultConnection))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    var paramEmail = new MySqlParameter("email", MySqlDbType.VarChar)
                    {
                        Value = email.Normalize()
                    };

                    cmd.Parameters.Add(paramEmail);

                    var result = Convert.ToString(cmd.ExecuteScalar());

                    conn.Close();

                    return result;
                }
            }
        }

        public async Task<string> FindIdByEmailAsync(string email)
        {
            return await Task.Run(async () =>
            {
                string sql = "SELECT Id FROM AspNetUsers WHERE NormalizedEmail = @email";

                using (MySqlConnection conn = new MySqlConnection(Options.DefaultConnection))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        var paramEmail = new MySqlParameter("email", MySqlDbType.VarChar)
                        {
                            Value = email.Normalize()
                        };

                        cmd.Parameters.Add(paramEmail);

                        var result = Convert.ToString(await cmd.ExecuteScalarAsync());

                        conn.Close();

                        return result;
                    }
                }
            });
        }

        public string FindIdByUsername(string username)
        {
            string sql = "SELECT Id FROM AspNetUsers WHERE NormalizedUserName = @username";

            using (MySqlConnection conn = new MySqlConnection(Options.DefaultConnection))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    var paramUsername = new MySqlParameter("username", MySqlDbType.VarChar)
                    {
                        Value = username.Normalize()
                    };

                    cmd.Parameters.Add(paramUsername);

                    var result = Convert.ToString(cmd.ExecuteScalar());

                    conn.Close();

                    return result;
                }
            }
        }

        public async Task<string> FindIdByUsernameAsync(string username)
        {
            return await Task.Run(async () =>
            {
                string sql = "SELECT Id FROM AspNetUsers WHERE NormalizedUserName = @username";

                using (MySqlConnection conn = new MySqlConnection(Options.DefaultConnection))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        var paramUsername = new MySqlParameter("username", MySqlDbType.VarChar)
                        {
                            Value = username.Normalize()
                        };

                        cmd.Parameters.Add(paramUsername);

                        var result = Convert.ToString(await cmd.ExecuteScalarAsync());

                        conn.Close();

                        return result;
                    }
                }
            });
        }

        public string FindPasswordHashByEmail(string email)
        {
            string sql = "SELECT PasswordHash FROM AspNetUsers WHERE NormalizedEmail = @email";

            using (MySqlConnection conn = new MySqlConnection(Options.DefaultConnection))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    var paramEmail = new MySqlParameter("email", MySqlDbType.VarChar)
                    {
                        Value = email.Normalize()
                    };

                    cmd.Parameters.Add(paramEmail);

                    var result = Convert.ToString(cmd.ExecuteScalar());

                    conn.Close();

                    return result.Equals(string.Empty) ? null : result;
                }
            }
        }

        public async Task<string> FindPasswordHashByEmailAsync(string email)
        {
            return await Task.Run(async () =>
            {
                string sql = "SELECT PasswordHash FROM AspNetUsers WHERE NormalizedEmail = @email";

                using (MySqlConnection conn = new MySqlConnection(Options.DefaultConnection))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        var paramEmail = new MySqlParameter("email", MySqlDbType.VarChar)
                        {
                            Value = email.Normalize()
                        };

                        cmd.Parameters.Add(paramEmail);

                        var result = Convert.ToString(await cmd.ExecuteScalarAsync());

                        conn.Close();

                        return result.Equals(string.Empty) ? null : result;
                    }
                }
            });
        }

        public DateTime FindSubscriptionExpirationByEmail(string email)
        {
            string sql = "SELECT SubscriptionExpiration FROM AspNetUsers WHERE NormalizedEmail = @email";

            using (MySqlConnection conn = new MySqlConnection(Options.DefaultConnection))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    var paramEmail = new MySqlParameter("email", MySqlDbType.VarChar)
                    {
                        Value = email.Normalize()
                    };

                    cmd.Parameters.Add(paramEmail);

                    var result = Convert.ToString(cmd.ExecuteScalar());

                    conn.Close();

                    return result.Equals(string.Empty) ? DateTime.Now.AddDays(30) : DateTime.Parse(result);
                }
            }
        }

        public async Task<DateTime> FindSubscriptionExpirationByEmailAsync(string email)
        {
            return await Task.Run(async () =>
            {
                string sql = "SELECT SubscriptionExpiration FROM AspNetUsers WHERE NormalizedEmail = @email";

                using (MySqlConnection conn = new MySqlConnection(Options.DefaultConnection))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        var paramEmail = new MySqlParameter("email", MySqlDbType.VarChar)
                        {
                            Value = email.Normalize()
                        };

                        cmd.Parameters.Add(paramEmail);

                        var result = Convert.ToString(await cmd.ExecuteScalarAsync());

                        conn.Close();

                        return result.Equals(string.Empty) ? DateTime.Now.AddDays(30) : DateTime.Parse(result);
                    }
                }
            });
        }

        public DateTime FindSubscriptionExpirationById(string id)
        {
            string sql = "SELECT SubscriptionExpiration FROM AspNetUsers WHERE Id = @id";

            using (MySqlConnection conn = new MySqlConnection(Options.DefaultConnection))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    var paramId = new MySqlParameter("id", MySqlDbType.VarChar)
                    {
                        Value = id
                    };

                    cmd.Parameters.Add(paramId);

                    var result = Convert.ToString(cmd.ExecuteScalar());

                    conn.Close();

                    return result.Equals(string.Empty) ? DateTime.Now.AddDays(30) : DateTime.Parse(result);
                }
            }
        }

        public async Task<DateTime> FindSubscriptionExpirationByIdAsync(string id)
        {
            return await Task.Run(async () =>
            {
                string sql = "SELECT SubscriptionExpiration FROM AspNetUsers WHERE Id = @id";

                using (MySqlConnection conn = new MySqlConnection(Options.DefaultConnection))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        var paramId = new MySqlParameter("id", MySqlDbType.VarChar)
                        {
                            Value = id
                        };

                        cmd.Parameters.Add(paramId);

                        var result = Convert.ToString(await cmd.ExecuteScalarAsync());

                        conn.Close();

                        return result.Equals(string.Empty) ? DateTime.Now.AddDays(30) : DateTime.Parse(result);
                    }
                }
            });
        }

        public string FindUsernameByEmail(string email)
        {
            string sql = "SELECT UserName FROM AspNetUsers WHERE NormalizedEmail = @email";

            using (MySqlConnection conn = new MySqlConnection(Options.DefaultConnection))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    var paramEmail = new MySqlParameter("email", MySqlDbType.VarChar)
                    {
                        Value = email.Normalize()
                    };

                    cmd.Parameters.Add(paramEmail);

                    var result = Convert.ToString(cmd.ExecuteScalar());

                    conn.Close();

                    return result;
                }
            }
        }

        public async Task<string> FindUsernameByEmailAsync(string email)
        {
            return await Task.Run(async () =>
            {
                string sql = "SELECT UserName FROM AspNetUsers WHERE NormalizedEmail = @email";

                using (MySqlConnection conn = new MySqlConnection(Options.DefaultConnection))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        var paramEmail = new MySqlParameter("email", MySqlDbType.VarChar)
                        {
                            Value = email.Normalize()
                        };

                        cmd.Parameters.Add(paramEmail);

                        var result = Convert.ToString(await cmd.ExecuteScalarAsync());

                        conn.Close();

                        return result;
                    }
                }
            });
        }

        public void SetSubscriptionExpirationById(string id, DateTime dateTime)
        {
            string sql = "UPDATE AspNetUsers SET SubscriptionExpiration = @dateTime WHERE Id = @id";

            using (MySqlConnection conn = new MySqlConnection(Options.DefaultConnection))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    var paramDateTime = new MySqlParameter("dateTime", MySqlDbType.DateTime)
                    {
                        Value = dateTime
                    };
                    var paramId = new MySqlParameter("id", MySqlDbType.VarChar)
                    {
                        Value = id
                    };

                    cmd.Parameters.Add(paramDateTime);
                    cmd.Parameters.Add(paramId);

                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }

        public async Task SetSubscriptionExpirationByIdAsync(string id, DateTime dateTime)
        {
            await Task.Run(async () =>
            {
                string sql = "UPDATE AspNetUsers SET SubscriptionExpiration = @dateTime WHERE Id = @id";

                using (MySqlConnection conn = new MySqlConnection(Options.DefaultConnection))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        var paramDateTime = new MySqlParameter("dateTime", MySqlDbType.DateTime)
                        {
                            Value = dateTime
                        };
                        var paramId = new MySqlParameter("id", MySqlDbType.VarChar)
                        {
                            Value = id
                        };

                        cmd.Parameters.Add(paramDateTime);
                        cmd.Parameters.Add(paramId);

                        await cmd.ExecuteNonQueryAsync();

                        conn.Close();
                    }
                }
            });
        }
    }
}
