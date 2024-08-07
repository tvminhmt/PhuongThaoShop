
export interface ServiceResponse {
  flag: boolean;
  message: string;
}
export interface PagedRequest {
  skipCount: number;
  maxResultCount: number;
}
export interface PagedResultDto<T> {
  totalCount: number;
  items: T[];
}
export interface LoginRequestDto {
  username: string | undefined;
  password: string | undefined;
}
export interface ProductDetailDto {
  id: string;
  code: string;
  oldPrice: number;
  price: number;
  newPrice: number;
  availableQuantity: number;
  status: number;
  upgrade?: string;
  description?: string;
  maRam?: string;
  thongSoRam?: string;
  thongSoHardDrive?: string;
  maHardDrive?: string;
  maCpu?: string;
  tenCpu?: string;
  nameColor?: string;
  maColor?: string;
  nameProduct?: string;
  nameManufacturer?: string;
  nameProductType?: string;
  maManHinh?: string;
  kichCoManHinh?: string;
  tanSoManHinh?: string;
  chatLieuManHinh?: string;
  maCardVGA?: string;
  tenCardVGA?: string;
  thongSoCardVGA?: string;
  imageMain?: string;
  listImage: string[];
  phanTramGiamGia: number;
}
export interface ProductDetailGetPageDto {
  stt: number;
  id: number;
  code?: string;
  oldPrice: number;
  price: number;
  discount: number;
  availableQuantity: number;
  status: number;
  strStatus?: string;
  upgrade?: string;
  description?: string;
  maRam?: string;
  thongSoRam?: string;
  thongSoHardDrive?: string;
  maHardDrive?: string;
  maCpu?: string;
  tenCpu?: string;
  nameColor?: string;
  maColor?: string;
  nameProduct?: string;
  nameManufacturer?: string;
  nameProductType?: string;
  maManHinh?: string;
  kichCoManHinh?: string;
  tanSoManHinh?: string;
  chatLieuManHinh?: string;
  maCardVGA?: string;
  tenCardVGA?: string;
  thongSoCardVGA?: string;
  imageMain?: string;
  otherImages?: string[];
  phanTramGiamGia: number;
}
export interface ResponseDto {
  result: any;
  isSuccess: boolean;
  code: number;
  message: string;
  count: number;
}
export interface CartItemDto {
  id: number;
  userId?: number;
  idProductDetails: number;
  quantity: number;
  maProductDetail?: string;
  newPrice: number;
  description?: string;
  maRam?: string;
  thongSoRam?: string;
  thongSoHardDrive?: string;
  maHardDrive?: string;
  maCpu?: string;
  tenCpu?: string;
  nameColor?: string;
  maColor?: string;
  nameProduct?: string;
  nameManufacturer?: string;
  maManHinh?: string;
  kichCoManHinh?: string;
  tanSoManHinh?: string;
  chatLieuManHinh?: string;
  maCardVGA?: string;
  tenCardVGA?: string;
  thongSoCardVGA?: string;
  linkImage?: string;
  status?: number;
}
export interface VoucherDto {
  id: number;
  CrDateTime: Date | null;
  status: number | null;
  maVoucher: string | null;
  tenVoucher: string | null;
  startDay: Date | null;
  endDay: Date | null;
  giaTri: number;
  soLuong: number;
}
export interface VoucherCreateDto {
  id: number;
  maVoucher: string | null;
  tenVoucher: string | null;
  starDay: Date | null;
  endDay: Date | null;
  giaTri: number;
  soLuong: number;
}
export interface RamDto {
  id: number;
  ma: string;
  thongSo?: string;
}
export interface CpuDto {
  id: number;
  ma: string;
  ten?: string;
}
export interface CardVGADto {
  id: number;
  ma: string;
  thongSo?: string;
  crUserId?: number;
  crDateTime?: Date;
  status: number;
}
export interface HardDriveDto {
  id: number;
  ma: string;
  thongSo?: string;
  crUserId?: number;
  crDateTime?: Date;
  status: number;
}
export interface ScreenDto {
  id: number;
  ma: string;
  kichCo?: string;
  tanSo?: string;
  chatLieu?: string;
  crUserId?: number;
  crDateTime?: Date;
  status: number;
}
export interface ColorDto {
  id: number;
  ma: string;
  name?: string;
  crUserId?: number;
  crDateTime?: Date;
  status: number;
}
export interface ProductDto {
  id: number;
  name: string;
  crUserId?: number;
  crDateTime?: Date;
  status: number;
}
export interface DiscountDto {
  id: number;
  code: string;
  Percentage: number;
}
export interface BillDto {
  id: number;
  invoiceCode: string;
  createDate?: Date;
  phoneNumber?: string;
  fullName?: string;
  address?: string;
  codeVoucher?: string;
  giamGia?: number;
  payment: number;
  stringPayment?: string;
  isPayment: boolean;
  userId?: number;
  status?: number;
  billDetail?: any;
  count: number;
}
export interface BillGetPageDto {
  id: number;
  stt: number;
  invoiceCode: string;
  crDateTime?: Date;
  updDateTime?: Date;
  phoneNumber?: string;
  fullName?: string;
  address?: string;
  codeVoucher?: string;
  giamGia?: number;
  payment: number;
  strPayment?: string;
  strStatus: string;
  isPayment: boolean;
  strIsPayment : string
  userId?: number;
  status: number;
  billDetail?: any;
  count: number;
  crUserName: string;
  updUserName: string;
}
export interface BillDetail {
  id: number;
  code: string;
  codeProductDetail?: string;
  quantity: number;
  price: number;
  billEntityId: number;
  crUserId?: number;
  status: number;
}
export interface RoleDto {
  id: number;
  code?: string;
  name?: string;
}
export interface PBillCreateCommand {
  phoneNumber?: string;
  fullName?: string;
  address?: string;
  userName?: string;
  codeVoucher?: string;
  payment: number;
  cartItem?: CartItemDto[];
}
export interface SerialDto {
  id: number;
  serialNumber: string;
  crUserId?: number | null;
  crDateTime?: Date | null;
  updUserId?: number | null;
  updDateTime?: Date | null;
  status: number;
  productDetailEntityId?: number | null;
  billDetailEntityId?: number | null;
}

// Example: api-result.model.ts

export interface ApiResult<T> {
  isSuccessed: boolean;
  message: string;
  resultObj: T;
}
export interface PBillGetByCodeQueryDto {
  invoiceCode: string;
  createDate?: Date | null;
  phoneNumber?: string; 
  fullName?: string; 
  address?: string; 
  codeVoucher?: string; 
  giamGia?: number | null; 
  payment?: number;
  isPayment?: boolean;
  userId?: number | null; 
  status?: number | null; 
  billDetail?: any; 
  count: number;
}
export interface ImageDto {
  id: number;
  name: string;
  url: string;
  description?: string; 
  crUserId?: number; 
  crDateTime?: Date; 
  status: number;
}
export interface AddressDto {
  addressId: number;
  addressName?: string;
  crUserId?: number;
  crDateTime?: Date;
  status: number;
  userEntityId?: number;
}
