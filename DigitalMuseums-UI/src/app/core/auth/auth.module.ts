import { NgModule } from '@angular/core';
import { AuthServiceConfig, GoogleLoginProvider, SocialLoginModule } from 'angularx-social-login';
import { environment } from '../../../environments/environment.prod';

const socialAuthConfig: AuthServiceConfig = new AuthServiceConfig([
  {
    id: GoogleLoginProvider.PROVIDER_ID,
    provider: new GoogleLoginProvider(environment.googleClientId),
  },
]);

@NgModule({
  imports: [SocialLoginModule],
  providers: [
    {
      provide: AuthServiceConfig,
      useFactory: () => socialAuthConfig,
    },
  ],
})
export class AuthModule {}
