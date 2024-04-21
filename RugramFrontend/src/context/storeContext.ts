import { createContext } from "react";
import UserStore from "../stores/userStore";
import ModalStore from "../stores/ModalStore";
import UploadStore from "../stores/UploadStore";

export const storeContext = createContext({
  userStore: new UserStore(),
  modalStore: new ModalStore(),
  uploadStore: new UploadStore(),
});
