import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UiPrimeModule } from './shared/ui-prime.module';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, UiPrimeModule],
  templateUrl: './app.component.html',
})
export class AppComponent {
  title = 'Frontend';
}
