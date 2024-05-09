import { App } from 'antd';
import { ItemType } from 'antd/es/menu/hooks/useItems';
import { useTranslation } from 'react-i18next';

import { useDeleteCourseTabMutation } from '../../../api/course-tabs.api';
import { DeleteIcon, EditIcon } from '../../../components';
import { useAppDispatch } from '../../../hooks/redux-hooks';
import useDisplayError from '../../../hooks/use-display-error';
import { CourseTab } from '../../../models/course-tab.interface';
import { setActiveCourseTab, setModalMode } from '../course-edit.slice';
import { CourseTabModalMode } from '../modals/tab-modal-mode.enum';

export default function useTabActions(tab: CourseTab): ItemType[] {
  const { t } = useTranslation();
  const appDispatch = useAppDispatch();

  const { modal } = App.useApp();
  const { displayError } = useDisplayError();

  const [deleteCourseTab] = useDeleteCourseTabMutation();

  const changeModalMode = (modalMode: CourseTabModalMode) => {
    appDispatch(setActiveCourseTab(tab));
    appDispatch(setModalMode(modalMode));
  };

  return [
    {
      key: '1',
      icon: <EditIcon />,
      label: t('actions.edit'),
      onClick: () => changeModalMode(CourseTabModalMode.EditName),
    },
    {
      key: '2',
      icon: <DeleteIcon />,
      label: t('actions.delete'),
      onClick: () =>
        modal.confirm({
          title: t('course_edit_page.confirm_delete_section', {
            section: tab.name,
          }),
          content: t('actions.confirm_delete'),
          okButtonProps: { danger: true },
          onOk: () =>
            deleteCourseTab({ id: tab.id })
              .unwrap()
              .catch((error) => displayError(error)),
        }),
    },
  ];
}
