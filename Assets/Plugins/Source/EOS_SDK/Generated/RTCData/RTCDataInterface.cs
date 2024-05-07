// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.RTCData
{
	public sealed partial class RTCDataInterface : Handle
	{
		public RTCDataInterface()
		{
		}

		public RTCDataInterface(System.IntPtr innerHandle) : base(innerHandle)
		{
		}

		/// <summary>
		/// The most recent version of the <see cref="AddNotifyDataReceived" /> API.
		/// </summary>
		public const int AddnotifydatareceivedApiLatest = 1;

		/// <summary>
		/// The most recent version of the <see cref="AddNotifyParticipantUpdated" /> API.
		/// </summary>
		public const int AddnotifyparticipantupdatedApiLatest = 1;

		/// <summary>
		/// The maximum length of data chunk in bytes that can be sent and received
		/// </summary>
		public const int MaxPacketSize = 1170;

		/// <summary>
		/// The most recent version of the <see cref="SendData" /> API.
		/// </summary>
		public const int SenddataApiLatest = 1;

		/// <summary>
		/// The most recent version of the <see cref="UpdateReceiving" /> API.
		/// </summary>
		public const int UpdatereceivingApiLatest = 1;

		/// <summary>
		/// The most recent version of the <see cref="UpdateSending" /> API.
		/// </summary>
		public const int UpdatesendingApiLatest = 1;

		/// <summary>
		/// Register to receive notifications with remote data packet received.
		/// If the returned NotificationId is valid, you must call <see cref="RemoveNotifyDataReceived" /> when you no longer wish to
		/// have your CompletionDelegate called.
		/// The CompletionDelegate may be called from a thread other than the one from which the SDK is ticking.
		/// <seealso cref="Common.InvalidNotificationid" />
		/// <seealso cref="RemoveNotifyDataReceived" />
		/// </summary>
		/// <param name="clientData">Arbitrary data that is passed back in the CompletionDelegate</param>
		/// <param name="completionDelegate">The callback to be fired when a data packet is received</param>
		/// <returns>
		/// Notification ID representing the registered callback if successful, an invalid NotificationId if not
		/// </returns>
		public ulong AddNotifyDataReceived(ref AddNotifyDataReceivedOptions options, object clientData, OnDataReceivedCallback completionDelegate)
		{
			AddNotifyDataReceivedOptionsInternal optionsInternal = new AddNotifyDataReceivedOptionsInternal();
			optionsInternal.Set(ref options);

			var clientDataAddress = System.IntPtr.Zero;

			var completionDelegateInternal = new OnDataReceivedCallbackInternal(OnDataReceivedCallbackInternalImplementation);
			Helper.AddCallback(out clientDataAddress, clientData, completionDelegate, completionDelegateInternal);

			var funcResult = Bindings.EOS_RTCData_AddNotifyDataReceived(InnerHandle, ref optionsInternal, clientDataAddress, completionDelegateInternal);

			Helper.Dispose(ref optionsInternal);

			Helper.AssignNotificationIdToCallback(clientDataAddress, funcResult);

			return funcResult;
		}

		/// <summary>
		/// Register to receive notifications when a room participant data status is updated (f.e when connection state changes).
		/// 
		/// The notification is raised when the participant's data status is updated. In order not to miss any participant status changes, applications need to add the notification before joining a room.
		/// 
		/// If the returned NotificationId is valid, you must call <see cref="RemoveNotifyParticipantUpdated" /> when you no longer wish
		/// to have your CompletionDelegate called.
		/// <seealso cref="Common.InvalidNotificationid" />
		/// <seealso cref="RemoveNotifyParticipantUpdated" />
		/// <seealso cref="ParticipantUpdatedCallbackInfo" />
		/// <seealso cref="RTCDataStatus" />
		/// </summary>
		/// <param name="options">structure containing the parameters for the operation.</param>
		/// <param name="clientData">Arbitrary data that is passed back in the CompletionDelegate</param>
		/// <param name="completionDelegate">The callback to be fired when a participant changes data status</param>
		/// <returns>
		/// Notification ID representing the registered callback if successful, an invalid NotificationId if not
		/// </returns>
		public ulong AddNotifyParticipantUpdated(ref AddNotifyParticipantUpdatedOptions options, object clientData, OnParticipantUpdatedCallback completionDelegate)
		{
			AddNotifyParticipantUpdatedOptionsInternal optionsInternal = new AddNotifyParticipantUpdatedOptionsInternal();
			optionsInternal.Set(ref options);

			var clientDataAddress = System.IntPtr.Zero;

			var completionDelegateInternal = new OnParticipantUpdatedCallbackInternal(OnParticipantUpdatedCallbackInternalImplementation);
			Helper.AddCallback(out clientDataAddress, clientData, completionDelegate, completionDelegateInternal);

			var funcResult = Bindings.EOS_RTCData_AddNotifyParticipantUpdated(InnerHandle, ref optionsInternal, clientDataAddress, completionDelegateInternal);

			Helper.Dispose(ref optionsInternal);

			Helper.AssignNotificationIdToCallback(clientDataAddress, funcResult);

			return funcResult;
		}

		/// <summary>
		/// Unregister a previously bound notification handler from receiving remote data packets.
		/// </summary>
		/// <param name="notificationId">The Notification ID representing the registered callback</param>
		public void RemoveNotifyDataReceived(ulong notificationId)
		{
			Bindings.EOS_RTCData_RemoveNotifyDataReceived(InnerHandle, notificationId);

			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		/// <summary>
		/// Unregister a previously bound notification handler from receiving participant updated notifications
		/// </summary>
		/// <param name="notificationId">The Notification ID representing the registered callback</param>
		public void RemoveNotifyParticipantUpdated(ulong notificationId)
		{
			Bindings.EOS_RTCData_RemoveNotifyParticipantUpdated(InnerHandle, notificationId);

			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		/// <summary>
		/// Use this function to send a data packet to the rest of participants.
		/// </summary>
		/// <param name="options">structure containing the parameters for the operation.</param>
		/// <returns>
		/// <see cref="Result.Success" /> the data packet was queued for sending
		/// <see cref="Result.InvalidParameters" /> if any of the options are invalid
		/// <see cref="Result.NotFound" /> if the specified room was not found
		/// </returns>
		public Result SendData(ref SendDataOptions options)
		{
			SendDataOptionsInternal optionsInternal = new SendDataOptionsInternal();
			optionsInternal.Set(ref options);

			var funcResult = Bindings.EOS_RTCData_SendData(InnerHandle, ref optionsInternal);

			Helper.Dispose(ref optionsInternal);

			return funcResult;
		}

		/// <summary>
		/// Use this function to tweak incoming data options for a room.
		/// </summary>
		/// <param name="options">structure containing the parameters for the operation.</param>
		/// <param name="clientData">Arbitrary data that is passed back in the CompletionDelegate</param>
		/// <param name="completionDelegate">The callback to be fired when the operation completes, either successfully or in error</param>
		/// <returns>
		/// <see cref="Result.Success" /> if the operation succeeded
		/// <see cref="Result.InvalidParameters" /> if any of the parameters are incorrect
		/// <see cref="Result.NotFound" /> if either the local user or specified participant are not in the room
		/// </returns>
		public void UpdateReceiving(ref UpdateReceivingOptions options, object clientData, OnUpdateReceivingCallback completionDelegate)
		{
			UpdateReceivingOptionsInternal optionsInternal = new UpdateReceivingOptionsInternal();
			optionsInternal.Set(ref options);

			var clientDataAddress = System.IntPtr.Zero;

			var completionDelegateInternal = new OnUpdateReceivingCallbackInternal(OnUpdateReceivingCallbackInternalImplementation);
			Helper.AddCallback(out clientDataAddress, clientData, completionDelegate, completionDelegateInternal);

			Bindings.EOS_RTCData_UpdateReceiving(InnerHandle, ref optionsInternal, clientDataAddress, completionDelegateInternal);

			Helper.Dispose(ref optionsInternal);
		}

		/// <summary>
		/// Use this function to tweak outgoing data options for a room.
		/// </summary>
		/// <param name="options">structure containing the parameters for the operation.</param>
		/// <param name="clientData">Arbitrary data that is passed back in the CompletionDelegate</param>
		/// <param name="completionDelegate">The callback to be fired when the operation completes, either successfully or in error</param>
		/// <returns>
		/// <see cref="Result.Success" /> if the operation succeeded
		/// <see cref="Result.InvalidParameters" /> if any of the parameters are incorrect
		/// <see cref="Result.NotFound" /> if the local user is not in the room
		/// </returns>
		public void UpdateSending(ref UpdateSendingOptions options, object clientData, OnUpdateSendingCallback completionDelegate)
		{
			UpdateSendingOptionsInternal optionsInternal = new UpdateSendingOptionsInternal();
			optionsInternal.Set(ref options);

			var clientDataAddress = System.IntPtr.Zero;

			var completionDelegateInternal = new OnUpdateSendingCallbackInternal(OnUpdateSendingCallbackInternalImplementation);
			Helper.AddCallback(out clientDataAddress, clientData, completionDelegate, completionDelegateInternal);

			Bindings.EOS_RTCData_UpdateSending(InnerHandle, ref optionsInternal, clientDataAddress, completionDelegateInternal);

			Helper.Dispose(ref optionsInternal);
		}

		[MonoPInvokeCallback(typeof(OnDataReceivedCallbackInternal))]
		internal static void OnDataReceivedCallbackInternalImplementation(ref DataReceivedCallbackInfoInternal data)
		{
			OnDataReceivedCallback callback;
			DataReceivedCallbackInfo callbackInfo;
			if (Helper.TryGetCallback(ref data, out callback, out callbackInfo))
			{
				callback(ref callbackInfo);
			}
		}

		[MonoPInvokeCallback(typeof(OnParticipantUpdatedCallbackInternal))]
		internal static void OnParticipantUpdatedCallbackInternalImplementation(ref ParticipantUpdatedCallbackInfoInternal data)
		{
			OnParticipantUpdatedCallback callback;
			ParticipantUpdatedCallbackInfo callbackInfo;
			if (Helper.TryGetCallback(ref data, out callback, out callbackInfo))
			{
				callback(ref callbackInfo);
			}
		}

		[MonoPInvokeCallback(typeof(OnUpdateReceivingCallbackInternal))]
		internal static void OnUpdateReceivingCallbackInternalImplementation(ref UpdateReceivingCallbackInfoInternal data)
		{
			OnUpdateReceivingCallback callback;
			UpdateReceivingCallbackInfo callbackInfo;
			if (Helper.TryGetAndRemoveCallback(ref data, out callback, out callbackInfo))
			{
				callback(ref callbackInfo);
			}
		}

		[MonoPInvokeCallback(typeof(OnUpdateSendingCallbackInternal))]
		internal static void OnUpdateSendingCallbackInternalImplementation(ref UpdateSendingCallbackInfoInternal data)
		{
			OnUpdateSendingCallback callback;
			UpdateSendingCallbackInfo callbackInfo;
			if (Helper.TryGetAndRemoveCallback(ref data, out callback, out callbackInfo))
			{
				callback(ref callbackInfo);
			}
		}
	}
}