using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliUtils
{
    public static class ServiceLocator
    {
        // 存储 IoC 容器的服务提供器
        private static IServiceProvider _serviceProvider;

        // 在 IoC 容器初始化完成后调用，注入服务提供器
        public static void Initialize(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        // 获取指定类型的服务实例（支持泛型）
        public static T GetService<T>() where T : class
        {
            if (_serviceProvider == null)
                throw new InvalidOperationException("ServiceLocator 未初始化，请先调用 Initialize 方法");

            return _serviceProvider.GetService<T>();
        }
    }
}
