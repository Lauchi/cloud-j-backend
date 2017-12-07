// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace cloud_j_backend.DependencyResolution {
    using AudioEngine.Domain;
    using cloud_j_backend.Controllers.Volume;
    using CSCore;
    using CSCore.Codecs;
    using CSCore.SoundOut;
    using CSCore.Streams;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
    using System.Collections.Generic;

    public class DefaultRegistry : Registry {
        private SimpleMixer _mixer;
        private VolumeSource _volumeSource1;
        #region Constructors and Destructors

        public DefaultRegistry() {
            Scan(
                scan => {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });

            For<IMixer>().Singleton().Use<Mixer>();
        }

        #endregion
    }
}