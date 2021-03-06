﻿
namespace Durak.Common
{
    /// <summary>
    /// Represents the type for a given message
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// Sent by the host client to request that the game start      
        ///                                                             
        /// Payload:                                                    
        ///     boolean - start                                         
        /// </summary>
        HostReqStart = 31,
        /// <summary>
        /// Sent by the host client to request that a bot is added
        /// 
        /// Payload:
        ///     byte   - difficulty
        ///     string - botName
        /// </summary>
        HostReqAddBot               = 32,
        /// <summary>
        /// Sent by the host client to request that a bot is deleted
        /// 
        /// Payload:
        ///     byte   - playerId
        ///     string - reason
        /// </summary>
        HostReqKick                 = 33,
        /// <summary>
        /// Sent by host client to request serverside bot settings
        /// 
        /// Payload:
        ///     boolean - Simulate think time
        ///     int32   - Minimum think time
        ///     int32   - Maximum think time
        ///     float   - Default bot difficulty
        /// </summary>
        HostReqBotSettings          = 34,

        /// <summary>
        /// Sent by the server to notify that a game state parameter has changed
        /// 
        /// Payload:
        ///     string  - parameter name
        ///     byte    - parameter type
        ///     T       - parameter value
        /// </summary>
        GameStateChanged            = 50,
        /// <summary>
        /// Sent by the server to transfer the entire game state to a client
        /// 
        /// Payload:
        ///     int     - num params
        ///         string  - param 1 name
        ///         byte    - param 1 type
        ///         T       - param 1 value
        ///         string  - param 2 name
        ///         byte    - param 2 type
        ///         T       - param 2 value
        ///         ...
        /// </summary>
        FullGameStateTransfer       = 51,

        /// <summary>
        /// Sent by the client to request the state of the server (lobby, in game)
        /// </summary>
        RequestServerState          = 60,
        /// <summary>
        /// Sent by the server to notify the client that the server is in an invalid state for a request the client has made
        /// 
        /// Payload:
        ///     string  - reason
        /// </summary>
        InvalidServerState          = 61,
        /// <summary>
        /// Sent by the server to notify clients that the server's state has changed
        /// 
        /// Payload:
        ///     byte    - newState
        ///     string  - reason
        /// </summary>
        NotifyServerStateChanged    = 62,
        /// <summary>
        /// Sent by client to request a state parameter be set to a specific value
        /// 
        /// Payload:
        ///    - StateParameter -> the parameter to set
        /// </summary>
        RequestState                = 63,

        /// <summary>
        /// Sent by the server to notify clients that a player has joined
        /// 
        /// Payload:
        ///     byte    - playerId
        ///     string  - playerName
        ///     boolean - isBot
        ///     padding 
        /// </summary>
        PlayerJoined                = 130,
        /// <summary>
        /// Sent by the server to contify clients that a player has left
        /// 
        /// Payload:
        ///     byte    - playerId
        ///     string  - reason
        /// </summary>
        PlayerLeft                  = 131,
        /// <summary>
        /// Sent by the client to notify the server that a client is ready for the game.
        /// Sent by the server to notify clients of a player's ready status
        /// 
        /// Payload:
        ///     byte    - playerId
        ///     boolean - isReady
        /// </summary>
        PlayerReady                 = 132,
        /// <summary>
        /// Sent by the server to notify clients that a player has been kicked
        /// 
        /// Payload:
        ///     byte   - playerId
        ///     string - reason
        /// </summary>
        PlayerKicked                = 133,
        /// <summary>
        /// Send by the server to a client once it connects
        /// 
        /// Payload:
        ///     byte      - playerId
        ///     boolean   - isHost
        ///     padding
        ///     GameState - state
        /// </summary>
        PlayerConnectInfo           = 134,
        /// <summary>
        /// Sent by the server to clients when the host has changed
        /// 
        /// Payload:
        ///   byte    - new host ID
        /// </summary>
        HostChanged                 = 135,

        /// <summary>
        /// Sent by the server to a client when their hand has changed
        /// </summary>
        PlayerHandChanged           = 140,
        /// <summary>
        /// Sent by the server to all clients when another client's number of cards has changed
        /// </summary>
        CardCountChanged            = 141,

        /// <summary>
        /// Sent by the client to request a card to be played
        /// 
        /// Payload:
        ///     GameMove - move
        /// </summary>
        SendMove                    = 150,
        /// <summary>
        /// Sent by the server to notify a client that their requested move is invalid
        /// 
        /// Payload:
        ///     GameMove - move
        ///     string   - reason
        /// </summary>
        InvalidMove                 = 151,
        /// <summary>
        /// Sent by the server to ntify clients that a player has made a game move
        /// 
        /// Payload:
        ///     GameMove - move
        /// </summary>
        SucessfullMove              = 152,

        /// <summary>
        /// Sent by the clients and the server for chat messages
        /// 
        /// Payload:
        ///     String  - message
        /// </summary>
        PlayerChat                  = 200,

        /// <summary>
        /// Tells the game host that they cannot start the game
        /// 
        /// Payload: 
        ///     String  - reason
        /// </summary>
        CannotStart                 = 201
    }
}
