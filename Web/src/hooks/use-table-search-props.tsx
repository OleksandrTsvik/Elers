import { SearchOutlined } from '@ant-design/icons';
import { Button, Flex, Input, InputRef } from 'antd';
import { ColumnType } from 'antd/es/table';
import { FilterConfirmProps } from 'antd/es/table/interface';
import React, { useRef, useState } from 'react';
import Highlighter from 'react-highlight-words';
import { useTranslation } from 'react-i18next';

export type GetColumnSearchProps<T> = (
  dataIndex: keyof T,
  placeholder?: string,
) => ColumnType<T>;

type Filters<DataType> = { [key in keyof DataType]?: string };

type DataIndex<DataType> = keyof DataType;

export default function useTableSearchProps<DataType>(
  initFilters: Filters<DataType> = {},
) {
  const { t } = useTranslation();
  const [filters, setFilters] = useState(initFilters);
  const filterInput = useRef<InputRef>(null);

  const updateFilter = (key: DataIndex<DataType>, value?: string) => {
    setFilters((state) => ({
      ...state,
      [key]: value,
    }));
  };

  const onFilter = (
    selectedKeys: React.Key[],
    confirm: (param?: FilterConfirmProps) => void,
    dataIndex: DataIndex<DataType>,
    closeDropdown: boolean = true,
  ) => {
    confirm({ closeDropdown });
    updateFilter(dataIndex, selectedKeys[0] as string);
  };

  const resetFilter = (
    dataIndex: DataIndex<DataType>,
    confirm: (param?: FilterConfirmProps) => void,
    clearFilters: () => void,
    closeDropdown: boolean = true,
  ) => {
    clearFilters();
    confirm({ closeDropdown });
    updateFilter(dataIndex);
  };

  const getColumnSearchProps: GetColumnSearchProps<DataType> = (
    dataIndex,
    placeholder,
  ) => ({
    filterDropdown: ({
      setSelectedKeys,
      selectedKeys,
      confirm,
      clearFilters,
    }) => (
      <div style={{ padding: 8 }} onKeyDown={(e) => e.stopPropagation()}>
        <Input
          ref={filterInput}
          value={selectedKeys[0]}
          placeholder={placeholder}
          style={{ marginBottom: 8, display: 'block' }}
          onChange={(e) =>
            setSelectedKeys(e.target.value ? [e.target.value] : [])
          }
          onPressEnter={() => onFilter(selectedKeys, confirm, dataIndex)}
        />
        <Flex justify="flex-end" gap="small">
          <Button
            type="primary"
            size="small"
            icon={<SearchOutlined />}
            onClick={() => onFilter(selectedKeys, confirm, dataIndex)}
          >
            {t('actions.search')}
          </Button>
          <Button
            size="small"
            onClick={() =>
              clearFilters && resetFilter(dataIndex, confirm, clearFilters)
            }
          >
            {t('actions.reset')}
          </Button>
        </Flex>
      </div>
    ),
    filterIcon: (filtered: boolean) => (
      <SearchOutlined style={{ color: filtered ? '#1677ff' : undefined }} />
    ),
    onFilterDropdownOpenChange: (visible) => {
      if (visible) {
        setTimeout(() => filterInput.current?.select(), 100);
      }
    },
    render: (text?: string) => {
      const searchWord = filters[dataIndex];

      if (!searchWord) {
        return text;
      }

      return (
        <Highlighter
          autoEscape
          searchWords={[searchWord]}
          textToHighlight={text?.toString() ?? ''}
          highlightStyle={{ backgroundColor: '#ffc069', padding: 0 }}
        />
      );
    },
  });

  return {
    filters,
    getColumnSearchProps,
  };
}
