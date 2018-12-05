﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Logic
{
    [DataContract, KnownType(typeof(Train))]
    public abstract class Train
    {
        public enum TrainType
        {
            Express, Stopping, Sleeper
        }

    [DataMember]
        public TrainType Type { get; set; }

        private string _id;
        //todo think of a way to make unique ids
    [DataMember]
        public string ID
        {
            get => _id;
            set
            {
                if (value.Length != 4)
                {
                    throw new ArgumentException("ID must be a four-character code");
                }
                _id = value;
            }
        }

        private Station _originStation;
    [DataMember]
        public Station OriginStation
        {
            get => _originStation;
            set
            {
                if (ValidStations.Stations.Where(station => station.Type == Station.StationType.Endpoint).All(station => station != value))
                {
                    throw new ArgumentException("Invalid origin station");
                }
                if (value == _destinationStation)
                {
                    throw new ArgumentException("Origin station cannot be same as destination station!");
                }

                _originStation = value;
            }
        }

        private Station _destinationStation;
    [DataMember]
        public Station DestinationStation
        {
            get => _destinationStation;
            set
            {
                if (ValidStations.Stations.Where(station => station.Type == Station.StationType.Endpoint).All(station => station != value))
                {
                    throw new ArgumentException("Invalid destination station");
                }
                if (value == _originStation)
                {
                    throw new ArgumentException("Destination station cannot be same as origin station!");
                }

                _destinationStation = value;
            }
        }


    [DataMember]
        private List<Station> _intermediateStations;
        public virtual List<Station> IntermediateStations
        {
            get => _intermediateStations;
            set
            {

                    foreach (var intermediateStation in value)
                    {
                        if (ValidStations.Stations.Where(station => station.Type == Station.StationType.Intermediate).All(station => station != intermediateStation))
                        {
                            throw new ArgumentException("Invalid intermediate station");
                        }
                    }

                _intermediateStations = value;
            }
        }

        /// <summary>
        /// Gets or sets the departure date time. Merged departure date and time as this framework doesn't provide for regular train running times and there is therefore nothing to be gained by having separate Departure Date and Departure Time properties
        /// </summary>
        /// <value>
        /// The departure date time.
        /// </value>
    [DataMember]
        public virtual DateTime DepartureDateTime { get; set; }

    [DataMember]
        public bool OffersFirstClass { get; set; }

    [DataMember]
        private bool _sleeperBerth;
        public virtual bool SleeperBerth
        {
            get => _sleeperBerth;
            set => _sleeperBerth = false;
        }
    }
}
