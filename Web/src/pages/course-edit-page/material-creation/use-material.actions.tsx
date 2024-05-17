import Icon, {
  ExperimentOutlined,
  FileOutlined,
  LinkOutlined,
  SolutionOutlined,
} from '@ant-design/icons';
import { ItemType } from 'antd/es/menu/hooks/useItems';
import { MenuInfo } from 'rc-menu/es/interface';
import { useTranslation } from 'react-i18next';
import { IoTextSharp } from 'react-icons/io5';

import useNavigateFrom from '../../../hooks/use-navigate-from';

const basePath = '/courses/material/add';

export default function useMaterialActions(tabId: string): ItemType[] {
  const { t } = useTranslation();

  const navigateFrom = useNavigateFrom();

  const handleClick = ({ key }: MenuInfo) =>
    navigateFrom(basePath + `/${key}/` + tabId);

  return [
    {
      key: 'content',
      icon: <Icon component={IoTextSharp} />,
      label: t('course.material.content'),
      onClick: handleClick,
    },
    {
      key: 'assignment',
      icon: <SolutionOutlined />,
      label: t('course.material.assignment'),
      onClick: handleClick,
    },
    {
      key: 'file',
      icon: <FileOutlined />,
      label: t('course.material.file'),
      onClick: handleClick,
    },
    {
      key: 'test',
      icon: <ExperimentOutlined />,
      label: t('course.material.test'),
      onClick: handleClick,
    },
    {
      key: 'link',
      icon: <LinkOutlined />,
      label: t('course.material.link'),
      onClick: handleClick,
    },
  ];
}
