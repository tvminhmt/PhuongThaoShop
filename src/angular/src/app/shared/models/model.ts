
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
  code?: string;
  oldPrice: number;
  price: number;
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
  linkImage?: string;
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
  price: number;
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
  ma?: string;
  thongSo?: string;
}
export interface RoleDto {
  id: number;
  code?: string;
  name?: string;
}
export interface RequestBillDto {
  phoneNumber?: string;
  fullName?: string;
  address?: string;
  username?: string;
  codeVoucher?: string;
  payment: number;
  cartItem?: CartItemDto[];
}
