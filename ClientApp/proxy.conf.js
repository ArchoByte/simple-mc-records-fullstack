const { env } = require('process');

const backendDomain = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:31944';

const profilesDomain = 'https://api.mojang.com';
const profilesPath = '/users/profiles/minecraft';
const texturesDomain = 'https://sessionserver.mojang.com';
const texturesPath = '/session/minecraft/profile';

const PROXY_CONFIG = [
  {
    context: [
      profilesPath + '/*'
    ],
    proxyTimeout: 10000,
    target: profilesDomain,
    secure: true,
    changeOrigin: true,
    headers: {
      Connection: 'Keep-Alive'
    }
  },
  {
    context: [
      texturesPath + '/*'
    ],
    proxyTimeout: 10000,
    target: texturesDomain,
    secure: true,
    changeOrigin: true,
    headers: {
      Connection: 'Keep-Alive'
    }
  },
  {
    context: [
      "/api/**",
    ],
    proxyTimeout: 10000,
    target: backendDomain,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIG;
