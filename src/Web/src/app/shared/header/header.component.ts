import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [],
  templateUrl: './header.component.html',
  styleUrls: []
})
export class HeaderComponent {
  @Input({ required: true }) pageName!: string;
}
