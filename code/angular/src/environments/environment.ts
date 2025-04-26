import { Environment } from '@ctincsp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'Work',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:5010/',
    redirectUri: baseUrl,
    clientId: 'Work_App',
    responseType: 'code',
    scope: 'offline_access Work',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:5011',
      rootNamespace: 'CSP_NET.Work',
    },
  },
} as Environment;
