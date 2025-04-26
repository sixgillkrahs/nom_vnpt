import { Environment } from '@ctincsp/ng.core';

const baseUrl = 'http://10.0.10.80:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'Work',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://10.0.10.80:44367/',
    redirectUri: baseUrl,
    clientId: 'Work_App',
    responseType: 'code',
    scope: 'offline_access Work',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://10.0.10.80:44352',
      rootNamespace: 'CSP_NET.Work',
    },
  },
} as Environment;
