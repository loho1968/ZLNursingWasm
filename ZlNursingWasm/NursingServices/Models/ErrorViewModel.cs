using NursingCommon;using System;

namespace NursingServices.Models
{
        /// byte转对象
    [Serializable]
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string errofInfo { get; set; }
    }
}
