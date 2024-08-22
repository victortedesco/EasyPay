import { ComponentFixture, TestBed } from "@angular/core/testing";

import { AuthenticationPageComponent } from "./authentication-page.component";
import { appConfig } from "../../app.config";
import {
  HttpTestingController,
  provideHttpClientTesting,
} from "@angular/common/http/testing";

describe("AuthenticationPageComponent", () => {
  let component: AuthenticationPageComponent;
  let fixture: ComponentFixture<AuthenticationPageComponent>;
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AuthenticationPageComponent],
      providers: [provideHttpClientTesting(), ...appConfig.providers],
    }).compileComponents();

    fixture = TestBed.createComponent(AuthenticationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });
});
