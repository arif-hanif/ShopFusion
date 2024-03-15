terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~>3.96"
    }
  }
  backend "azurerm" {
  }
  required_version = "~>1.7.5"
}
provider "azurerm" {
  features {

  }
}