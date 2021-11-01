export enum Permissions {
  CreateCenter = ":Center:CreateCenter",
  EditCenter = ":Center:EditCenter",
  SearchCenter = ":Center:SearchCenter",
  RemoveCenter = ":Center:RemoveCenter",

  CreateCenterVariable = ":CenterVariable:CreateCenterVariable",
  EditCenterVariable = ":CenterVariable:EditCenterVariable",
  RemoveCenterVariable = ":CenterVariable:RemoveCenterVariable",
  SearchCenterVariable = ":CenterVariable:SearchCenterVariable",
  SortCenterVariables = ":CenterVariable:SortCenterVariables",
  GetCenterVariables = ":CenterVariable:GetCenterVariablesByParentId",

  CreateActivity = ":Activity:CreateActivity",
  RemoveActivity = ":Activity:RemoveActivity",
  CreateActivitiesByMainApplication = ":Activity:CreateActivitiesByMainApplication",
  GetActivitiesByCenterId = ":Activity:GetActivitiesByCenterId",
  GetActiveApplication = ':CenterVariable:GetCenterVariablesWithActiveApplications',

  UpdateAccess = ":DynamicRoleClaimsManager:UpdateAccess",
  UpdateAccessSubmit = ":DynamicRoleClaimsManager:UpdateAccessSubmit",
  Slog = ":Slog",
}
