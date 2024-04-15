import { makeAutoObservable } from "mobx";

class UserStore {
  id?: number;

  constructor() {
    makeAutoObservable(this)
  }
}

export default UserStore