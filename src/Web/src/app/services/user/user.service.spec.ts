import { TestBed } from "@angular/core/testing";

import { UserService } from "./user.service";
import { provideHttpClient, withFetch } from "@angular/common/http";

describe("UserService", () => {
  let service: UserService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [provideHttpClient(withFetch())],
    });
    service = TestBed.inject(UserService);
  });

  it("should be created", () => {
    expect(service).toBeTruthy();
  });
});
