using System;

namespace metaio
{
	// Same integer values as in the native enum ETRACKING_STATE
	public enum TrackingState
	{
		Unknown = 0,
		NotTracking = 1,
		Tracking = 2,
		Lost = 3,
		Found = 4,
		Extrapolated = 5,
		Initialized = 6,
	 	Registered = 7,
		InitializationFailed = 8
	}
	
	public static class TrackingStateExtensions
	{
		public static bool isTrackingState(this TrackingState state)
		{
			return state == TrackingState.Tracking || state == TrackingState.Found || state == TrackingState.Extrapolated;
		}
	}

	/// <summary>
	/// TrackingValues data structure.
	/// Exactly same as native structure, except rotation, which is quaternion in this structure.
	/// </summary>
	public class TrackingValues
	{
		public TrackingState state;
		public Vector3d translation;
		public Vector4d rotation;
		public LLACoordinate llaCoordinate;
		public float quality;
		public double timeElapsed;
		public double trackingTimeMs;
		public double timestampInSeconds;
		public int coordinateSystemID;
		public string cosName;
		public string additionalValues;
		public string sensor;
		
		public static TrackingValues FromPB(metaio.unitycommunication.TrackingValues tv)
		{
			TrackingValues ret = new TrackingValues();
			ret.state = (TrackingState)tv.State;
			ret.translation = new Vector3d(tv.Translation.X, tv.Translation.Y, tv.Translation.Z);
			ret.rotation = new Vector4d(tv.Rotation.X, tv.Rotation.Y, tv.Rotation.Z, tv.Rotation.W);
			ret.llaCoordinate = new LLACoordinate() 
			{
				latitude = tv.LlaCoordinate.Latitude,
				longitude = tv.LlaCoordinate.Longitude,
				altitude = tv.LlaCoordinate.Altitude,
				accuracy = tv.LlaCoordinate.Accuracy,
				timestamp = tv.LlaCoordinate.Timestamp
			};
			ret.quality = tv.Quality;
			ret.timeElapsed = tv.TimeElapsed;
			ret.trackingTimeMs = tv.TrackingTimeMs;
			ret.timestampInSeconds = tv.TimestampInSeconds;
			ret.coordinateSystemID = tv.CoordinateSystemID;
			ret.cosName = tv.CosName;
			ret.additionalValues = tv.AdditionalValues;
			ret.sensor = tv.Sensor;
			return ret;
		}
	}
}

