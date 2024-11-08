import { ComponentFixture, TestBed } from "@angular/core/testing";

import { ProfilePageComponent } from "./profile-page.component";
import { appConfig } from "../../app.config";
import {
  HttpTestingController,
  provideHttpClientTesting,
} from "@angular/common/http/testing";
import { UserService } from "../../services/user/user.service";

describe("ProfilePageComponent", () => {
  let component: ProfilePageComponent;
  let fixture: ComponentFixture<ProfilePageComponent>;
  let userService: UserService;
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProfilePageComponent],
      providers: [provideHttpClientTesting(), ...appConfig.providers],
    }).compileComponents();

    fixture = TestBed.createComponent(ProfilePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

    userService = TestBed.inject(UserService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });
});
