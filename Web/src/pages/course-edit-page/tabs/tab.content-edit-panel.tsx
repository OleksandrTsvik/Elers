import { DragOutlined, ScissorOutlined } from '@ant-design/icons';
import { Space, Tooltip, Button, Popconfirm } from 'antd';
import { DraggableProvidedDragHandleProps } from 'react-beautiful-dnd';
import { useTranslation } from 'react-i18next';

import { getCourseMaterialEditPagePath } from './tab.utils';
import {
  useDeleteCourseMaterialMutation,
  useUpdateCourseMaterialActiveMutation,
} from '../../../api/course-materials.mutations.api';
import { DeleteIcon, EditIcon, VisibilityIcon } from '../../../components';
import { useAppDispatch } from '../../../hooks/redux-hooks';
import useDisplayError from '../../../hooks/use-display-error';
import useNavigateFrom from '../../../hooks/use-navigate-from';
import { CourseMaterial } from '../../../models/course-material.type';
import { CourseMaterialIcon, useMaterialLabels } from '../../../shared';
import { setActiveCourseMaterial, setModalMode } from '../course-edit.slice';
import { CourseEditModalMode } from '../modals/edit-modal-mode.enum';

interface Props {
  material: CourseMaterial;
  dragHandleProps: DraggableProvidedDragHandleProps | null | undefined;
}

export default function TabContentEditPanel({
  material,
  dragHandleProps,
}: Props) {
  const { t } = useTranslation();
  const navigateFrom = useNavigateFrom();
  const appDispatch = useAppDispatch();

  const { displayError } = useDisplayError();
  const { getMaterialLabel } = useMaterialLabels();

  const [updateCourseMaterialActive, { isLoading: isUpdateActiveLoading }] =
    useUpdateCourseMaterialActiveMutation();

  const [deleteCourseMaterial] = useDeleteCourseMaterialMutation();

  const handleMoveMaterialClick = () => {
    appDispatch(setActiveCourseMaterial(material));
    appDispatch(setModalMode(CourseEditModalMode.MoveMaterial));
  };

  const handleUpdateCourseMaterialActive = async () => {
    await updateCourseMaterialActive({
      id: material.id,
      isActive: !material.isActive,
    })
      .unwrap()
      .catch((error) => displayError(error));
  };

  const handleDeleteCourseMaterial = async () => {
    await deleteCourseMaterial({ id: material.id })
      .unwrap()
      .catch((error) => displayError(error));
  };

  const handleClinkEdit = () => {
    navigateFrom(
      getCourseMaterialEditPagePath(
        material.type,
        material.courseTabId,
        material.id,
      ),
    );
  };

  return (
    <Space.Compact block size="small">
      <Tooltip title={t('course_edit_page.drag_to_move')}>
        <Button icon={<DragOutlined />} {...dragHandleProps} />
      </Tooltip>
      <Tooltip title={getMaterialLabel(material.type)}>
        <Button icon={<CourseMaterialIcon type={material.type} />} disabled />
      </Tooltip>
      <Tooltip
        title={
          material.isActive
            ? t('course_edit_page.visible_material')
            : t('course_edit_page.invisible_material')
        }
      >
        <Button
          loading={isUpdateActiveLoading}
          icon={<VisibilityIcon visibility={material.isActive} />}
          onClick={handleUpdateCourseMaterialActive}
        />
      </Tooltip>
      <Tooltip title={t('course_edit_page.move_to_another_section')}>
        <Button icon={<ScissorOutlined />} onClick={handleMoveMaterialClick} />
      </Tooltip>
      <Tooltip title={t('actions.edit')}>
        <Button icon={<EditIcon />} onClick={handleClinkEdit} />
      </Tooltip>
      <Tooltip title={t('actions.delete')}>
        <Popconfirm
          title={t('actions.confirm_deletion')}
          onConfirm={handleDeleteCourseMaterial}
        >
          <Button icon={<DeleteIcon />} />
        </Popconfirm>
      </Tooltip>
    </Space.Compact>
  );
}
