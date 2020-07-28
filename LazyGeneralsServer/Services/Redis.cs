using LazyGeneralsServer.Models.Lobby;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LazyGeneralsServer.Services
{
    //Client:ClientId
    //ClientId:InGame
    //Lobby:LobbyId
    //LobbyId_host:ClientId
    //LobbyId_client:ClientId
    //LobbyId_pass:Password
    //LobbyId_name:Name
    //LobbyId_game:GameId
    //Game:GameId
    //GameId:ArmyId
    //ArmyId:ClientId
    //ArmyId:Health
    //ArmyId:PosX
    //ArmyId:PosY
    public interface IRedis
    {
        public Lobby GetLobby(string name);
        public void SetLobby(Lobby l);
    }

    public class Redis
    {
        private readonly IConnectionMultiplexer _redisConnection;

        public Redis(IConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
        }

        //public Lobby GetLobby(string name)
        //{
        //}

        public void SetLobby(Lobby l)
        {

        }
    }
}
