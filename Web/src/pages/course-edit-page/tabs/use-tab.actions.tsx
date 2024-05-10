import { App } from 'antd';
import { ItemType } from 'antd/es/menu/hooks/useItems';
import { useTranslation } from 'react-i18next';

import {
  useDeleteCourseTabMutation,
  useUpdateCourseTabMutation,
} from '../../../api/course-tabs.api';
import {
  ColorIcon,
  CountIcon,
  DeleteIcon,
  EditIcon,
  VisibilityIcon,
} from '../../../components';
import { useAppDispatch } from '../../../hooks/redux-hooks';
import useDisplayError from '../../../hooks/use-display-error';
import { CourseTab } from '../../../models/course-tab.interface';
import { setActiveCourseTab, setModalMode } from '../course-edit.slice';
import { CourseTabModalMode } from '../modals/tab-modal-mode.enum';

export default function useTabActions(tab: CourseTab) {
  const { t } = useTranslation();
  const appDispatch = useAppDispatch();

  const { modal } = App.useApp();
  const { displayError } = useDisplayError();

  const [updateCourseTab, { isLoading }] = useUpdateCourseTabMutation();
  const [deleteCourseTab] = useDeleteCourseTabMutation();

  const changeModalMode = (modalMode: CourseTabModalMode) => {
    appDispatch(setActiveCourseTab(tab));
    appDispatch(setModalMode(modalMode));
  };

  const handleChangeVisibilityClick = async () => {
    await updateCourseTab({ tabId: tab.id, isActive: !tab.isActive }).unwrap();
  };

  const handleShowMaterialsCountClick = async () => {
    await updateCourseTab({
      tabId: tab.id,
      showMaterialsCount: !tab.showMaterialsCount,
    }).unwrap();
  };

  const handleDeleteClick = async () => {
    await modal.confirm({
      title: t('course_edit_page.confirm_delete_section', {
        section: tab.name,
      }),
      content: t('actions.confirm_delete'),
      okButtonProps: { danger: true },
      onOk: () =>
        deleteCourseTab({ id: tab.id })
          .unwrap()
          .catch((error) => displayError(error)),
    });
  };

  const tabActions: ItemType[] = [
    {
      key: 'edit',
      icon: <EditIcon />,
      label: t('actions.edit'),
      onClick: () => changeModalMode(CourseTabModalMode.EditName),
    },
    {
      key: 'changeColor',
      icon: <ColorIcon />,
      label: t('actions.change_color'),
      onClick: () => changeModalMode(CourseTabModalMode.EditColor),
    },
    {
      key: 'changeVisibility',
      icon: <VisibilityIcon visibility={!tab.isActive} />,
      label: tab.isActive
        ? t('actions.make_invisible')
        : t('actions.make_visible'),
      onClick: handleChangeVisibilityClick,
    },
    {
      key: 'showMaterialsCount',
      icon: <CountIcon />,
      label: tab.showMaterialsCount
        ? t('course_edit_page.not_show_materials_count')
        : t('course_edit_page.show_materials_count'),
      onClick: handleShowMaterialsCountClick,
    },
    {
      key: 'delete',
      icon: <DeleteIcon />,
      label: t('actions.delete'),
      onClick: handleDeleteClick,
    },
  ];

  return { tabActions, isLoading };
}
