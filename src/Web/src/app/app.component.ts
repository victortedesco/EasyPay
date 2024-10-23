import { APP_INITIALIZER, Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ConfigService } from './services/config.service';
import { tap } from 'rxjs';
import { BrowserModule } from '@angular/platform-browser';

export function initializeApp(configService: ConfigService) {
  return () =>
    configService.loadConfig().pipe(
      tap(config => configService.setConfig(config))
    ).toPromise();
}

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [BrowserModule, RouterOutlet],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: initializeApp,
      deps: [ConfigService],
      multi: true,
    },
  ],
  templateUrl: './app.component.html',
})
export class AppComponent {
  title = 'EasyPay';
}
