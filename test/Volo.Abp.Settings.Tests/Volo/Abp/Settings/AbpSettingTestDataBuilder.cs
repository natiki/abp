﻿using System;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Session;

namespace Volo.Abp.Settings
{
    public class AbpSettingTestDataBuilder : ITransientDependency
    {
        public static Guid User1Id = Guid.NewGuid();
        public static Guid User2Id = Guid.NewGuid();

        private readonly ISettingRepository _settingRepository;
        private readonly IGuidGenerator _guidGenerator;

        public AbpSettingTestDataBuilder(ISettingRepository settingRepository, IGuidGenerator guidGenerator)
        {
            _settingRepository = settingRepository;
            _guidGenerator = guidGenerator;
        }

        public void Build()
        {
            _settingRepository.InsertAsync(new Setting(_guidGenerator.Create(), "MySetting1", "42", GlobalSettingValueProvider.ProviderName));

            _settingRepository.InsertAsync(new Setting(_guidGenerator.Create(), "MySetting2", "default-store-value", GlobalSettingValueProvider.ProviderName));
            _settingRepository.InsertAsync(new Setting(_guidGenerator.Create(), "MySetting2", "user1-store-value", UserSettingValueProvider.ProviderName, User1Id.ToString()));
            _settingRepository.InsertAsync(new Setting(_guidGenerator.Create(), "MySetting2", "user2-store-value", UserSettingValueProvider.ProviderName, User2Id.ToString()));

            _settingRepository.InsertAsync(new Setting(_guidGenerator.Create(), "MySettingWithoutInherit", "default-store-value", GlobalSettingValueProvider.ProviderName));
            _settingRepository.InsertAsync(new Setting(_guidGenerator.Create(), "MySettingWithoutInherit", "user1-store-value", UserSettingValueProvider.ProviderName, User1Id.ToString()));
        }
    }
}