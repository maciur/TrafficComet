using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using TrafficComet.Abstracts.Accessors;

namespace TrafficComet.Core
{
	public class TrafficCometMiddlewaresAccessor : ITrafficCometMiddlewaresAccessor
	{
		protected const string CONTEXT_ITEM_KEY = "TrafficCometValues";

		public string ApplicationId
		{
			get => Values.ApplicationId;
			internal set => Values.ApplicationId = value;
		}

		public string ClientId
		{
			get => Values.ClientId;
			internal set => Values.ClientId = value;
		}

		public IDictionary<string, string> CustomParams
		{
			get => Values.CustomParams;
			set => Values.CustomParams = value;
		}

		public DateTime EndDateLocal
		{
			get => Values.EndDateLocal;
			internal set => Values.EndDateLocal = value;
		}

		public DateTime EndDateUtc
		{
			get => Values.EndDateUtc;
			internal set => Values.EndDateUtc = value;
		}

		public bool IgnoreRequest
		{
			get => Values.IgnoreRequest;
			internal set => Values.IgnoreRequest = value;
		}

		public bool IgnoreResponse
		{
			get => Values.IgnoreResponse;
			internal set => Values.IgnoreResponse = value;
		}

		public bool IgnoreWholeRequest
		{
			get => Values.IgnoreWholeRequest;
			internal set => Values.IgnoreWholeRequest = value;
		}

		public dynamic RequestBody
		{
			get => Values.RequestBody;
			internal set => Values.RequestBody = value;
		}

		public IDictionary<string, string> RequestCustomParams
		{
			get => Values.RequestCustomParams;
			set => Values.RequestCustomParams = value;
		}

		public dynamic ResponseBody
		{
			get => Values.ResponseBody;
			internal set => Values.ResponseBody = value;
		}

		public IDictionary<string, string> ResponseCustomParams
		{
			get => Values.ResponseCustomParams;
			set => Values.ResponseCustomParams = value;
		}

		public DateTime StartDateLocal
		{
			get => Values.StartDateLocal;
			internal set => Values.StartDateLocal = value;
		}

		public DateTime StartDateUtc
		{
			get => Values.StartDateUtc;
			internal set => Values.StartDateUtc = value;
		}

		public string TraceId
		{
			get => Values.TraceId;
			internal set => Values.TraceId = value;
		}

		protected IHttpContextAccessor HttpContextAccessor { get; }

		private TrafficCometContextValues Values
			=> (TrafficCometContextValues)HttpContextAccessor.HttpContext.Items[CONTEXT_ITEM_KEY];

		public TrafficCometMiddlewaresAccessor(IHttpContextAccessor httpContextAccessor)
		{
			HttpContextAccessor = httpContextAccessor
				?? throw new ArgumentNullException(nameof(httpContextAccessor));
		}

		protected internal void InitContextValues()
		{
			HttpContextAccessor.HttpContext.Items.Add(CONTEXT_ITEM_KEY, new TrafficCometContextValues());
		}
	}
}