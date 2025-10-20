import { ApplicationConfig, inject, provideAppInitializer, provideBrowserGlobalErrorListeners, provideZonelessChangeDetection } from '@angular/core';
import { provideRouter, withViewTransitions } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { InitService } from '../services/init-service';
import { lastValueFrom } from 'rxjs';
import { jwtInterceptor } from '../interceptors/jwt-interceptor';

export const appConfig: ApplicationConfig = {
	providers: [
		provideBrowserGlobalErrorListeners(),
		provideZonelessChangeDetection(),
		provideRouter(routes, withViewTransitions()),
		provideHttpClient(withInterceptors([jwtInterceptor])),
		provideAppInitializer(() => {
			const initService = inject(InitService)

			return lastValueFrom(initService.start())
		})
	]
};