import { Address } from "./address.model"
import { Gender } from "./gender.model"

export interface Student {
  id: string,
  firstName: string,
  lastName: string,
  birthDate: string,
  email: string,
  phone: string,
  profileImageUrl: string,
  genderId: string,
  gender: Gender,
  addressId: string,
  address: Address
}
