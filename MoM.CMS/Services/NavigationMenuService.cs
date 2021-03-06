﻿using MoM.CMS.Interfaces;
using System.Collections.Generic;
using System.Linq;
using MoM.CMS.Dtos;
using MoM.Module.Interfaces;
using System;

namespace MoM.CMS.Services
{
    public class NavigationMenuService : INavigationMenuService
    {
        private IDataStorage Storage;

        public NavigationMenuService(IDataStorage storage)
        {
            Storage = storage;
        }

        public NavigationMenuDto GetMenuByName(string name)
        {
            return Storage.GetRepository<INavigationMenuRepository>().Fetch(n => n.Name.Equals(name)).FirstOrDefault().ToDTO() ;
        }


        public IEnumerable<NavigationMenuItemDto> GetMenuItemsByMenuNameAndMenuItemId(string name, int id, string routeName)
        {
            var menu = Storage.GetRepository<INavigationMenuRepository>().Fetch(m => m.Name.ToLower() == name.ToLower() ).FirstOrDefault();
            if (menu == null)
            {
                return null;
            }
            var currentItem = id == 0 ?
                routeName == null ? 
                Storage.GetRepository<INavigationMenuItemRepository>()
                                .Fetch(i => i.Parent == null
                                && i.NavigationMenuId == menu.NavigationMenuId).FirstOrDefault()
                                :
                                Storage.GetRepository<INavigationMenuItemRepository>()
                                .Fetch(i => i.Name == routeName
                                && i.NavigationMenuId == menu.NavigationMenuId).FirstOrDefault()
                :
                Storage.GetRepository<INavigationMenuItemRepository>()
                                .Fetch(i => i.NavigationMenuItemId == id 
                                && i.NavigationMenu.NavigationMenuId == menu.NavigationMenuId).FirstOrDefault();
            var result = new List<NavigationMenuItemDto>();
            if (currentItem == null)
                return result;
            var parentItem = currentItem != null 
                                && currentItem.ParentNavigationMenuItemId != null ? Storage.GetRepository<INavigationMenuItemRepository>()
                                    .Fetch(i => i.NavigationMenuItemId == currentItem.ParentNavigationMenuItemId
                                    && i.NavigationMenuId == menu.NavigationMenuId).FirstOrDefault()
                                : null;
            var menuItems = currentItem != null ? Storage.GetRepository<INavigationMenuItemRepository>().Fetch(
                                i => i.Parent != null 
                                && i.Parent.NavigationMenuItemId == currentItem.NavigationMenuItemId 
                                && i.NavigationMenuId == menu.NavigationMenuId)
                            .OrderBy(x => x.SortOrder).ToDTOs() : null; 

            if(menuItems.Count == 0) //has no children present siblings
            {
                var parentsParentItem = parentItem != null ? Storage.GetRepository<INavigationMenuItemRepository>().Fetch(i => i.NavigationMenuItemId == parentItem.ParentNavigationMenuItemId).FirstOrDefault() : null;
                menuItems = Storage.GetRepository<INavigationMenuItemRepository>().Fetch(i => i.ParentNavigationMenuItemId == currentItem.ParentNavigationMenuItemId).OrderBy(x => x.ParentNavigationMenuItemId).ThenBy(y => y.SortOrder).ToDTOs();
                if (parentsParentItem != null)
                {
                    var parentsParentDto = parentsParentItem.ToDTO();
                    parentsParentDto.displayName = string.Empty;
                    parentsParentDto.isParent = true;
                    parentsParentDto.iconClass = "fa fa-level-up";
                    result.Add(parentsParentDto);
                }
                if (parentItem != null)
                {
                    var parentDto = parentItem.ToDTO();
                    result.Add(parentDto);
                }
            }
            else
            {
                if (parentItem != null)
                {
                    var parentDto = parentItem.ToDTO();
                    parentDto.displayName = string.Empty;
                    parentDto.isParent = true;
                    parentDto.iconClass = "fa fa-level-up";
                    result.Add(parentDto);
                }
                result.Add(currentItem.ToDTO());
            }

            foreach (var item in menuItems)
            {
                result.Add(item);
            }
            return result;
        }

        public IEnumerable<NavigationMenuDto> GetMenus()
        {
            return Storage.GetRepository<INavigationMenuRepository>().Table().OrderByDescending(x => x.DisplayName).ToDTOs();
        }
    }
}
