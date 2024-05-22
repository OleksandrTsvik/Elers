import { ItemType } from 'antd/es/menu/hooks/useItems';
import { MenuInfo } from 'rc-menu/es/interface';

import useNavigateFrom from '../../../hooks/use-navigate-from';
import {
  CourseMaterialType,
  CourseMaterialIcon,
  useMaterialLabels,
} from '../../../shared';

const basePath = '/courses/material/add';

export default function useMaterialActions(tabId: string): ItemType[] {
  const { getMaterialLabel } = useMaterialLabels();

  const navigateFrom = useNavigateFrom();

  const handleClick = ({ key }: MenuInfo) =>
    navigateFrom(basePath + `/${key}/` + tabId);

  return [
    {
      key: 'content',
      icon: <CourseMaterialIcon type={CourseMaterialType.Content} />,
      label: getMaterialLabel(CourseMaterialType.Content),
      onClick: handleClick,
    },
    {
      key: 'assignment',
      icon: <CourseMaterialIcon type={CourseMaterialType.Assignment} />,
      label: getMaterialLabel(CourseMaterialType.Assignment),
      onClick: handleClick,
    },
    {
      key: 'file',
      icon: <CourseMaterialIcon type={CourseMaterialType.File} />,
      label: getMaterialLabel(CourseMaterialType.File),
      onClick: handleClick,
    },
    {
      key: 'test',
      icon: <CourseMaterialIcon type={CourseMaterialType.Test} />,
      label: getMaterialLabel(CourseMaterialType.Test),
      onClick: handleClick,
    },
    {
      key: 'link',
      icon: <CourseMaterialIcon type={CourseMaterialType.Link} />,
      label: getMaterialLabel(CourseMaterialType.Link),
      onClick: handleClick,
    },
  ];
}
