using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChuanLeMaClient.Common
{
    /// <summary>
    /// 进度流包装器 用于上传下载进度监控
    /// </summary>
    public class ProgressStream : Stream
    {
        private readonly Stream _innerStream;
        private readonly Action<long> _progressCallback;

        public ProgressStream(Stream innerStream, Action<long> progressCallback)
        {
            _innerStream = innerStream;
            _progressCallback = progressCallback;
        }
        public override int Read(byte[] buffer, int offset, int count)
        {
            var bytesRead = _innerStream.Read(buffer, offset, count);
            if (bytesRead > 0) _progressCallback?.Invoke(bytesRead);
            return bytesRead;
        }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            var bytesRead = await _innerStream.ReadAsync(buffer, offset, count, cancellationToken);
            if (bytesRead > 0) _progressCallback?.Invoke(bytesRead);
            return bytesRead;
        }

        // 实现其他必要的Stream成员
        public override bool CanRead => _innerStream.CanRead;
        public override bool CanSeek => _innerStream.CanSeek;
        public override bool CanWrite => _innerStream.CanWrite;
        public override long Length => _innerStream.Length;
        public override long Position
        {
            get => _innerStream.Position;
            set => _innerStream.Position = value;
        }

        public override void Flush() => _innerStream.Flush();
        public override long Seek(long offset, SeekOrigin origin) => _innerStream.Seek(offset, origin);
        public override void SetLength(long value) => _innerStream.SetLength(value);
        public override void Write(byte[] buffer, int offset, int count) => _innerStream.Write(buffer, offset, count);
    }
}
